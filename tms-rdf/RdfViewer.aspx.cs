using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;

using RDFSharp.Model;
using RDFSharp.Store;
using RDFSharp.Query;


namespace tms_rdf
{
    public partial class RdfViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // IMPORTANT:   This is just a demo!  Run LoadRDF() ONCE to create the data you need in the database and as an RDF file.
            //              Comment out LoadRDF() and uncomment GetRDF() once the data are loaded.

            //LoadRDF();
            GetRDF();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            GetRDF();
        }

        protected void GetRDF()
        {
            try
            {

                // First we set some core RDF resources
                // agent refers to artist(s)
                RDFResource type = RDFVocabulary.RDF.TYPE;
                RDFResource name = RDFVocabulary.FOAF.NAME;
                RDFResource agent = RDFVocabulary.FOAF.AGENT;

                // TGN is a Getty vocabulary for locations
                // TGN added to RDFSharp vocabularies manually.  TGN ID is stored in TMSThes (or equivalent in TMS 2014+)
                RDFResource tgn = new RDFResource(RDFVocabulary.TGN.BASE_URI);

                // The predicates below are often part of ULAN (which we don't have)
                // unsure if using bio events this way is acceptable...

                RDFResource livedIn = new RDFResource("http://purl.org/vocab/bio/0.1/event/livedIn");
                RDFResource activeIn = new RDFResource("http://purl.org/vocab/bio/0.1/event/activeIn");
                RDFResource educatedIn = new RDFResource("http://purl.org/vocab/bio/0.1/event/educatedIn");
                RDFResource bornIn = new RDFResource("http://purl.org/vocab/bio/0.1/event/bornIn");
                RDFResource diedIn = new RDFResource("http://purl.org/vocab/bio/0.1/event/diedIn");

                RDFResource anUri = new RDFResource("http://example.org/uris#anUri");

                // Create variables
                RDFVariable x = new RDFVariable("x", true);
                RDFVariable y = new RDFVariable("y", true);
                RDFVariable n = new RDFVariable("n", true);
                RDFVariable h = new RDFVariable("h", true);
                RDFVariable p = new RDFVariable("p", true);

                string m_select = DropDownList1.Text;
                string m_filter = TextBox1.Text;

                RDFResource m_sparql_resource = livedIn;

                if (m_select == "livedIn")
                {
                    m_sparql_resource = livedIn;
                }
                if (m_select == "activeIn")
                {
                    m_sparql_resource = activeIn;
                }
                if (m_select == "educatedIn")
                {
                    m_sparql_resource = educatedIn;
                }
                if (m_select == "bornIn")
                {
                    m_sparql_resource = bornIn;
                }
                if (m_select == "diedIn")
                {
                    m_sparql_resource = diedIn;
                }

                // Create Sparql Select query
                RDFSelectQuery q1 = new RDFSelectQuery()
                .AddPatternGroup(new RDFPatternGroup("TmsPatternGroup")
                .AddPattern(new RDFPattern(y, m_sparql_resource, x))
                .AddPattern(new RDFPattern(x, type, tgn))
                .AddPattern(new RDFPattern(y, name, h).Optional())
                .AddPattern(new RDFPattern(x, name, n).Optional())
                .AddFilter(new RDFRegexFilter(n, new Regex(m_filter, RegexOptions.IgnoreCase))))
                .AddModifier(new RDFDistinctModifier());

                // Apply query
                string m_conn = ConfigurationManager.ConnectionStrings["RDFConnectionString"].ConnectionString;
                RDFSQLServerStore rdf_mssql = new RDFSQLServerStore(m_conn);
                RDFSelectQueryResult res = q1.ApplyToStore(rdf_mssql);

                Label1.Text = "RESULTS: " + res.SelectResultsCount + "\n\n" + q1;
                GridView1.DataSource = res.SelectResults;
                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                string m_debug = ex.Message;
            }


        }

        protected void LoadRDF()
        {

            // First we set some core RDF resources
            // agent refers to artist(s)
            RDFResource type = RDFVocabulary.RDF.TYPE;
            RDFResource name = RDFVocabulary.FOAF.NAME;
            RDFResource agent = RDFVocabulary.FOAF.AGENT;

            // TGN is a Getty vocabulary for locations
            // TGN added to RDFSharp vocabularies manually.  TGN ID is stored in TMSThes (or equivalent in TMS 2014+)
            RDFResource tgn = new RDFResource(RDFVocabulary.TGN.BASE_URI);

            // The predicates below are often part of ULAN (which we don't have)
            // unsure if using bio events this way is acceptable...

            RDFResource livedIn = new RDFResource("http://purl.org/vocab/bio/0.1/event/livedIn");
            RDFResource activeIn = new RDFResource("http://purl.org/vocab/bio/0.1/event/activeIn");
            RDFResource educatedIn = new RDFResource("http://purl.org/vocab/bio/0.1/event/educatedIn");
            RDFResource bornIn = new RDFResource("http://purl.org/vocab/bio/0.1/event/bornIn");
            RDFResource diedIn = new RDFResource("http://purl.org/vocab/bio/0.1/event/diedIn");

            RDFResource anUri = new RDFResource("http://example.org/uris#anUri");


            // The following entries are all generated from SQL queries which you can find in the file SQL.txt  Technically we could load them into the database directly,
            // but this allows us to see how a resource is "built" and it creates the correct RDF format files (NTriples, RdfXml, TriX), as well as inserts into the db.
            // Once in the db, we do not need to run LoadRDF() except to refresh data.

            #region Artists (agents)
            // Ideally this would reference the Getty vocabulary for ULAN but we don't have the ULAN ID stored in the db
            RDFResource raimundabraham = new RDFResource("http://example.org/artists/47");
            RDFResource emilioambasz = new RDFResource("http://example.org/artists/141");
            RDFResource mariobotta = new RDFResource("http://example.org/artists/696");
            RDFResource louisikahn = new RDFResource("http://example.org/artists/2964");
            RDFResource frederickkiesler = new RDFResource("http://example.org/artists/3091");
            RDFResource leonkrier = new RDFResource("http://example.org/artists/3250");
            RDFResource masayukikurokawa = new RDFResource("http://example.org/artists/3308");
            RDFResource ernestobrunolapadula = new RDFResource("http://example.org/artists/3381");
            RDFResource richardmeier = new RDFResource("http://example.org/artists/3910");
            RDFResource eerosaarinen = new RDFResource("http://example.org/artists/5103");
            RDFResource franklloydwright = new RDFResource("http://example.org/artists/6459");
            RDFResource evazeisel = new RDFResource("http://example.org/artists/6556");
            RDFResource zahahadid = new RDFResource("http://example.org/artists/6953");
            RDFResource santiagocalatrava = new RDFResource("http://example.org/artists/6968");
            RDFResource tadaoando = new RDFResource("http://example.org/artists/7055");
            RDFResource ludwigmiesvanderrohe = new RDFResource("http://example.org/artists/7166");
            RDFResource rafaelviñoly = new RDFResource("http://example.org/artists/7229");
            RDFResource aldorossi = new RDFResource("http://example.org/artists/7661");
            RDFResource simonungers = new RDFResource("http://example.org/artists/7992");
            RDFResource giovanniguerrini = new RDFResource("http://example.org/artists/8157");
            RDFResource marioromano = new RDFResource("http://example.org/artists/8158");
            RDFResource thommayne = new RDFResource("http://example.org/artists/8218");
            RDFResource thomaskinslow = new RDFResource("http://example.org/artists/8248");
            RDFResource fusogomuindcoltdtokyo = new RDFResource("http://example.org/artists/9029");
            RDFResource hallchinacoeastliverpooloh = new RDFResource("http://example.org/artists/10013");
            RDFResource andrewzago = new RDFResource("http://example.org/artists/22884");
            RDFResource morphosissantamonicaca = new RDFResource("http://example.org/artists/29711");

            // Artists(agents): plain literal name

            RDFPlainLiteral aldorossiName = new RDFPlainLiteral("Aldo Rossi");
            RDFPlainLiteral andrewzagoName = new RDFPlainLiteral("Andrew Zago");
            RDFPlainLiteral eerosaarinenName = new RDFPlainLiteral("Eero Saarinen");
            RDFPlainLiteral emilioambaszName = new RDFPlainLiteral("Emilio Ambasz");
            RDFPlainLiteral ernestobrunolapadulaName = new RDFPlainLiteral("Ernesto Bruno La Padula");
            RDFPlainLiteral evazeiselName = new RDFPlainLiteral("Eva Zeisel");
            RDFPlainLiteral franklloydwrightName = new RDFPlainLiteral("Frank Lloyd Wright");
            RDFPlainLiteral frederickkieslerName = new RDFPlainLiteral("Frederick Kiesler");
            RDFPlainLiteral fusogomuindcoltdtokyoName = new RDFPlainLiteral("Fuso Gomu Ind. Co., Ltd., Tokyo");
            RDFPlainLiteral giovanniguerriniName = new RDFPlainLiteral("Giovanni Guerrini");
            RDFPlainLiteral hallchinacoeastliverpoolohName = new RDFPlainLiteral("Hall China Co., East Liverpool, OH");
            RDFPlainLiteral leonkrierName = new RDFPlainLiteral("Leon Krier");
            RDFPlainLiteral louisikahnName = new RDFPlainLiteral("Louis I. Kahn");
            RDFPlainLiteral ludwigmiesvanderroheName = new RDFPlainLiteral("Ludwig Mies van der Rohe");
            RDFPlainLiteral mariobottaName = new RDFPlainLiteral("Mario Botta");
            RDFPlainLiteral marioromanoName = new RDFPlainLiteral("Mario Romano");
            RDFPlainLiteral masayukikurokawaName = new RDFPlainLiteral("Masayuki Kurokawa");
            RDFPlainLiteral morphosissantamonicacaName = new RDFPlainLiteral("Morphosis, Santa Monica, CA");
            RDFPlainLiteral rafaelviñolyName = new RDFPlainLiteral("Rafael Viñoly");
            RDFPlainLiteral raimundabrahamName = new RDFPlainLiteral("Raimund Abraham");
            RDFPlainLiteral richardmeierName = new RDFPlainLiteral("Richard Meier");
            RDFPlainLiteral santiagocalatravaName = new RDFPlainLiteral("Santiago Calatrava");
            RDFPlainLiteral simonungersName = new RDFPlainLiteral("Simon Ungers");
            RDFPlainLiteral tadaoandoName = new RDFPlainLiteral("Tadao Ando");
            RDFPlainLiteral thommayneName = new RDFPlainLiteral("Thom Mayne");
            RDFPlainLiteral thomaskinslowName = new RDFPlainLiteral("Thomas Kinslow");
            RDFPlainLiteral zahahadidName = new RDFPlainLiteral("Zaha Hadid");

            #endregion

            #region Locations (tgn)

            RDFResource northwales = new RDFResource("http://vocab.getty.edu/tgn/2091433-place");
            RDFResource wales = new RDFResource("http://vocab.getty.edu/tgn/7002443-place");
            RDFResource berlin = new RDFResource("http://vocab.getty.edu/tgn/7003712-place");
            RDFResource newyork = new RDFResource("http://vocab.getty.edu/tgn/7007567-place");
            RDFResource edinburgh = new RDFResource("http://vocab.getty.edu/tgn/7009546-place");
            RDFResource kiyev = new RDFResource("http://vocab.getty.edu/tgn/7010171-place");
            RDFResource london = new RDFResource("http://vocab.getty.edu/tgn/7011781-place");
            RDFResource moskva = new RDFResource("http://vocab.getty.edu/tgn/7012974-place");
            RDFResource boston = new RDFResource("http://vocab.getty.edu/tgn/7013445-place");
            RDFResource detroit = new RDFResource("http://vocab.getty.edu/tgn/7013547-place");
            RDFResource longbeach = new RDFResource("http://vocab.getty.edu/tgn/7013905-place");
            RDFResource sanfrancisco = new RDFResource("http://vocab.getty.edu/tgn/7014456-place");

            // Locations (tgn): plain literal name

            RDFPlainLiteral berlinName = new RDFPlainLiteral("Berlin");
            RDFPlainLiteral bostonName = new RDFPlainLiteral("Boston");
            RDFPlainLiteral detroitName = new RDFPlainLiteral("Detroit");
            RDFPlainLiteral edinburghName = new RDFPlainLiteral("Edinburgh");
            RDFPlainLiteral kiyevName = new RDFPlainLiteral("Kiyev");
            RDFPlainLiteral londonName = new RDFPlainLiteral("London");
            RDFPlainLiteral longbeachName = new RDFPlainLiteral("Long Beach");
            RDFPlainLiteral moskvaName = new RDFPlainLiteral("Moskva");
            RDFPlainLiteral newyorkName = new RDFPlainLiteral("New York");
            RDFPlainLiteral northwalesName = new RDFPlainLiteral("North Wales");
            RDFPlainLiteral sanfranciscoName = new RDFPlainLiteral("San Francisco");
            RDFPlainLiteral walesName = new RDFPlainLiteral("Wales");

            #endregion

            #region Triples

            // Create triple resources

            RDFTriple aldorossi_type_agent = new RDFTriple(aldorossi, type, agent);
            RDFTriple andrewzago_type_agent = new RDFTriple(andrewzago, type, agent);
            RDFTriple eerosaarinen_type_agent = new RDFTriple(eerosaarinen, type, agent);
            RDFTriple emilioambasz_type_agent = new RDFTriple(emilioambasz, type, agent);
            RDFTriple ernestobrunolapadula_type_agent = new RDFTriple(ernestobrunolapadula, type, agent);
            RDFTriple evazeisel_type_agent = new RDFTriple(evazeisel, type, agent);
            RDFTriple franklloydwright_type_agent = new RDFTriple(franklloydwright, type, agent);
            RDFTriple frederickkiesler_type_agent = new RDFTriple(frederickkiesler, type, agent);
            RDFTriple fusogomuindcoltdtokyo_type_agent = new RDFTriple(fusogomuindcoltdtokyo, type, agent);
            RDFTriple giovanniguerrini_type_agent = new RDFTriple(giovanniguerrini, type, agent);
            RDFTriple hallchinacoeastliverpooloh_type_agent = new RDFTriple(hallchinacoeastliverpooloh, type, agent);
            RDFTriple leonkrier_type_agent = new RDFTriple(leonkrier, type, agent);
            RDFTriple louisikahn_type_agent = new RDFTriple(louisikahn, type, agent);
            RDFTriple ludwigmiesvanderrohe_type_agent = new RDFTriple(ludwigmiesvanderrohe, type, agent);
            RDFTriple mariobotta_type_agent = new RDFTriple(mariobotta, type, agent);
            RDFTriple marioromano_type_agent = new RDFTriple(marioromano, type, agent);
            RDFTriple masayukikurokawa_type_agent = new RDFTriple(masayukikurokawa, type, agent);
            RDFTriple morphosissantamonicaca_type_agent = new RDFTriple(morphosissantamonicaca, type, agent);
            RDFTriple rafaelviñoly_type_agent = new RDFTriple(rafaelviñoly, type, agent);
            RDFTriple raimundabraham_type_agent = new RDFTriple(raimundabraham, type, agent);
            RDFTriple richardmeier_type_agent = new RDFTriple(richardmeier, type, agent);
            RDFTriple santiagocalatrava_type_agent = new RDFTriple(santiagocalatrava, type, agent);
            RDFTriple simonungers_type_agent = new RDFTriple(simonungers, type, agent);
            RDFTriple tadaoando_type_agent = new RDFTriple(tadaoando, type, agent);
            RDFTriple thommayne_type_agent = new RDFTriple(thommayne, type, agent);
            RDFTriple thomaskinslow_type_agent = new RDFTriple(thomaskinslow, type, agent);
            RDFTriple zahahadid_type_agent = new RDFTriple(zahahadid, type, agent);

            RDFTriple berlin_type_tgn = new RDFTriple(berlin, type, tgn);
            RDFTriple boston_type_tgn = new RDFTriple(boston, type, tgn);
            RDFTriple detroit_type_tgn = new RDFTriple(detroit, type, tgn);
            RDFTriple edinburgh_type_tgn = new RDFTriple(edinburgh, type, tgn);
            RDFTriple kiyev_type_tgn = new RDFTriple(kiyev, type, tgn);
            RDFTriple london_type_tgn = new RDFTriple(london, type, tgn);
            RDFTriple longbeach_type_tgn = new RDFTriple(longbeach, type, tgn);
            RDFTriple moskva_type_tgn = new RDFTriple(moskva, type, tgn);
            RDFTriple newyork_type_tgn = new RDFTriple(newyork, type, tgn);
            RDFTriple northwales_type_tgn = new RDFTriple(northwales, type, tgn);
            RDFTriple sanfrancisco_type_tgn = new RDFTriple(sanfrancisco, type, tgn);
            RDFTriple wales_type_tgn = new RDFTriple(wales, type, tgn);

            RDFTriple aldorossi_name_aldorossiName = new RDFTriple(aldorossi, name, aldorossiName);
            RDFTriple andrewzago_name_andrewzagoName = new RDFTriple(andrewzago, name, andrewzagoName);
            RDFTriple eerosaarinen_name_eerosaarinenName = new RDFTriple(eerosaarinen, name, eerosaarinenName);
            RDFTriple emilioambasz_name_emilioambaszName = new RDFTriple(emilioambasz, name, emilioambaszName);
            RDFTriple ernestobrunolapadula_name_ernestobrunolapadulaName = new RDFTriple(ernestobrunolapadula, name, ernestobrunolapadulaName);
            RDFTriple evazeisel_name_evazeiselName = new RDFTriple(evazeisel, name, evazeiselName);
            RDFTriple franklloydwright_name_franklloydwrightName = new RDFTriple(franklloydwright, name, franklloydwrightName);
            RDFTriple frederickkiesler_name_frederickkieslerName = new RDFTriple(frederickkiesler, name, frederickkieslerName);
            RDFTriple fusogomuindcoltdtokyo_name_fusogomuindcoltdtokyoName = new RDFTriple(fusogomuindcoltdtokyo, name, fusogomuindcoltdtokyoName);
            RDFTriple giovanniguerrini_name_giovanniguerriniName = new RDFTriple(giovanniguerrini, name, giovanniguerriniName);
            RDFTriple hallchinacoeastliverpooloh_name_hallchinacoeastliverpoolohName = new RDFTriple(hallchinacoeastliverpooloh, name, hallchinacoeastliverpoolohName);
            RDFTriple leonkrier_name_leonkrierName = new RDFTriple(leonkrier, name, leonkrierName);
            RDFTriple louisikahn_name_louisikahnName = new RDFTriple(louisikahn, name, louisikahnName);
            RDFTriple ludwigmiesvanderrohe_name_ludwigmiesvanderroheName = new RDFTriple(ludwigmiesvanderrohe, name, ludwigmiesvanderroheName);
            RDFTriple mariobotta_name_mariobottaName = new RDFTriple(mariobotta, name, mariobottaName);
            RDFTriple marioromano_name_marioromanoName = new RDFTriple(marioromano, name, marioromanoName);
            RDFTriple masayukikurokawa_name_masayukikurokawaName = new RDFTriple(masayukikurokawa, name, masayukikurokawaName);
            RDFTriple morphosissantamonicaca_name_morphosissantamonicacaName = new RDFTriple(morphosissantamonicaca, name, morphosissantamonicacaName);
            RDFTriple rafaelviñoly_name_rafaelviñolyName = new RDFTriple(rafaelviñoly, name, rafaelviñolyName);
            RDFTriple raimundabraham_name_raimundabrahamName = new RDFTriple(raimundabraham, name, raimundabrahamName);
            RDFTriple richardmeier_name_richardmeierName = new RDFTriple(richardmeier, name, richardmeierName);
            RDFTriple santiagocalatrava_name_santiagocalatravaName = new RDFTriple(santiagocalatrava, name, santiagocalatravaName);
            RDFTriple simonungers_name_simonungersName = new RDFTriple(simonungers, name, simonungersName);
            RDFTriple tadaoando_name_tadaoandoName = new RDFTriple(tadaoando, name, tadaoandoName);
            RDFTriple thommayne_name_thommayneName = new RDFTriple(thommayne, name, thommayneName);
            RDFTriple thomaskinslow_name_thomaskinslowName = new RDFTriple(thomaskinslow, name, thomaskinslowName);
            RDFTriple zahahadid_name_zahahadidName = new RDFTriple(zahahadid, name, zahahadidName);

            RDFTriple berlin_name_berlinName = new RDFTriple(berlin, name, berlinName);
            RDFTriple boston_name_bostonName = new RDFTriple(boston, name, bostonName);
            RDFTriple detroit_name_detroitName = new RDFTriple(detroit, name, detroitName);
            RDFTriple edinburgh_name_edinburghName = new RDFTriple(edinburgh, name, edinburghName);
            RDFTriple kiyev_name_kiyevName = new RDFTriple(kiyev, name, kiyevName);
            RDFTriple london_name_londonName = new RDFTriple(london, name, londonName);
            RDFTriple longbeach_name_longbeachName = new RDFTriple(longbeach, name, longbeachName);
            RDFTriple moskva_name_moskvaName = new RDFTriple(moskva, name, moskvaName);
            RDFTriple newyork_name_newyorkName = new RDFTriple(newyork, name, newyorkName);
            RDFTriple northwales_name_northwalesName = new RDFTriple(northwales, name, northwalesName);
            RDFTriple sanfrancisco_name_sanfranciscoName = new RDFTriple(sanfrancisco, name, sanfranciscoName);
            RDFTriple wales_name_walesName = new RDFTriple(wales, name, walesName);

            RDFTriple aldorossi_activeIn_moskva = new RDFTriple(aldorossi, activeIn, moskva);
            RDFTriple andrewzago_activeIn_moskva = new RDFTriple(andrewzago, activeIn, moskva);
            RDFTriple eerosaarinen_activeIn_newyork = new RDFTriple(eerosaarinen, activeIn, newyork);
            RDFTriple emilioambasz_activeIn_newyork = new RDFTriple(emilioambasz, activeIn, newyork);
            RDFTriple ernestobrunolapadula_activeIn_newyork = new RDFTriple(ernestobrunolapadula, activeIn, newyork);
            RDFTriple evazeisel_educatedIn_berlin = new RDFTriple(evazeisel, educatedIn, berlin);
            RDFTriple franklloydwright_activeIn_detroit = new RDFTriple(franklloydwright, activeIn, detroit);
            RDFTriple franklloydwright_livedIn_moskva = new RDFTriple(franklloydwright, livedIn, moskva);
            RDFTriple frederickkiesler_livedIn_longbeach = new RDFTriple(frederickkiesler, livedIn, longbeach);
            RDFTriple fusogomuindcoltdtokyo_livedIn_london = new RDFTriple(fusogomuindcoltdtokyo, livedIn, london);
            RDFTriple giovanniguerrini_activeIn_newyork = new RDFTriple(giovanniguerrini, activeIn, newyork);
            RDFTriple hallchinacoeastliverpooloh_educatedIn_berlin = new RDFTriple(hallchinacoeastliverpooloh, educatedIn, berlin);
            RDFTriple leonkrier_activeIn_newyork = new RDFTriple(leonkrier, activeIn, newyork);
            RDFTriple leonkrier_educatedIn_london = new RDFTriple(leonkrier, educatedIn, london);
            RDFTriple louisikahn_livedIn_longbeach = new RDFTriple(louisikahn, livedIn, longbeach);
            RDFTriple louisikahn_livedIn_newyork = new RDFTriple(louisikahn, livedIn, newyork);
            RDFTriple ludwigmiesvanderrohe_activeIn_moskva = new RDFTriple(ludwigmiesvanderrohe, activeIn, moskva);
            RDFTriple ludwigmiesvanderrohe_livedIn_moskva = new RDFTriple(ludwigmiesvanderrohe, livedIn, moskva);
            RDFTriple mariobotta_activeIn_kiyev = new RDFTriple(mariobotta, activeIn, kiyev);
            RDFTriple mariobotta_activeIn_moskva = new RDFTriple(mariobotta, activeIn, moskva);
            RDFTriple mariobotta_livedIn_moskva = new RDFTriple(mariobotta, livedIn, moskva);
            RDFTriple marioromano_activeIn_newyork = new RDFTriple(marioromano, activeIn, newyork);
            RDFTriple masayukikurokawa_livedIn_london = new RDFTriple(masayukikurokawa, livedIn, london);
            RDFTriple morphosissantamonicaca_activeIn_moskva = new RDFTriple(morphosissantamonicaca, activeIn, moskva);
            RDFTriple rafaelviñoly_activeIn_newyork = new RDFTriple(rafaelviñoly, activeIn, newyork);
            RDFTriple raimundabraham_livedIn_moskva = new RDFTriple(raimundabraham, livedIn, moskva);
            RDFTriple richardmeier_activeIn_edinburgh = new RDFTriple(richardmeier, activeIn, edinburgh);
            RDFTriple richardmeier_activeIn_london = new RDFTriple(richardmeier, activeIn, london);
            RDFTriple richardmeier_educatedIn_london = new RDFTriple(richardmeier, educatedIn, london);
            RDFTriple santiagocalatrava_livedIn_newyork = new RDFTriple(santiagocalatrava, livedIn, newyork);
            RDFTriple simonungers_livedIn_boston = new RDFTriple(simonungers, livedIn, boston);
            RDFTriple simonungers_livedIn_newyork = new RDFTriple(simonungers, livedIn, newyork);
            RDFTriple tadaoando_activeIn_newyork = new RDFTriple(tadaoando, activeIn, newyork);
            RDFTriple tadaoando_livedIn_northwales = new RDFTriple(tadaoando, livedIn, northwales);
            RDFTriple tadaoando_livedIn_wales = new RDFTriple(tadaoando, livedIn, wales);
            RDFTriple thommayne_activeIn_moskva = new RDFTriple(thommayne, activeIn, moskva);
            RDFTriple thomaskinslow_livedIn_boston = new RDFTriple(thomaskinslow, livedIn, boston);
            RDFTriple thomaskinslow_livedIn_newyork = new RDFTriple(thomaskinslow, livedIn, newyork);
            RDFTriple zahahadid_activeIn_sanfrancisco = new RDFTriple(zahahadid, activeIn, sanfrancisco);

            #endregion

            #region Add triples to graph

            RDFGraph m_graph = new RDFGraph();

            m_graph.AddTriple(aldorossi_activeIn_moskva);
            m_graph.AddTriple(aldorossi_name_aldorossiName);
            m_graph.AddTriple(aldorossi_type_agent);
            m_graph.AddTriple(andrewzago_activeIn_moskva);
            m_graph.AddTriple(andrewzago_name_andrewzagoName);
            m_graph.AddTriple(andrewzago_type_agent);
            m_graph.AddTriple(berlin_name_berlinName);
            m_graph.AddTriple(berlin_type_tgn);
            m_graph.AddTriple(boston_name_bostonName);
            m_graph.AddTriple(boston_type_tgn);
            m_graph.AddTriple(detroit_name_detroitName);
            m_graph.AddTriple(detroit_type_tgn);
            m_graph.AddTriple(edinburgh_name_edinburghName);
            m_graph.AddTriple(edinburgh_type_tgn);
            m_graph.AddTriple(eerosaarinen_activeIn_newyork);
            m_graph.AddTriple(eerosaarinen_name_eerosaarinenName);
            m_graph.AddTriple(eerosaarinen_type_agent);
            m_graph.AddTriple(emilioambasz_activeIn_newyork);
            m_graph.AddTriple(emilioambasz_name_emilioambaszName);
            m_graph.AddTriple(emilioambasz_type_agent);
            m_graph.AddTriple(ernestobrunolapadula_activeIn_newyork);
            m_graph.AddTriple(ernestobrunolapadula_name_ernestobrunolapadulaName);
            m_graph.AddTriple(ernestobrunolapadula_type_agent);
            m_graph.AddTriple(evazeisel_educatedIn_berlin);
            m_graph.AddTriple(evazeisel_name_evazeiselName);
            m_graph.AddTriple(evazeisel_type_agent);
            m_graph.AddTriple(franklloydwright_activeIn_detroit);
            m_graph.AddTriple(franklloydwright_livedIn_moskva);
            m_graph.AddTriple(franklloydwright_name_franklloydwrightName);
            m_graph.AddTriple(franklloydwright_type_agent);
            m_graph.AddTriple(frederickkiesler_livedIn_longbeach);
            m_graph.AddTriple(frederickkiesler_name_frederickkieslerName);
            m_graph.AddTriple(frederickkiesler_type_agent);
            m_graph.AddTriple(fusogomuindcoltdtokyo_livedIn_london);
            m_graph.AddTriple(fusogomuindcoltdtokyo_name_fusogomuindcoltdtokyoName);
            m_graph.AddTriple(fusogomuindcoltdtokyo_type_agent);
            m_graph.AddTriple(giovanniguerrini_activeIn_newyork);
            m_graph.AddTriple(giovanniguerrini_name_giovanniguerriniName);
            m_graph.AddTriple(giovanniguerrini_type_agent);
            m_graph.AddTriple(hallchinacoeastliverpooloh_educatedIn_berlin);
            m_graph.AddTriple(hallchinacoeastliverpooloh_name_hallchinacoeastliverpoolohName);
            m_graph.AddTriple(hallchinacoeastliverpooloh_type_agent);
            m_graph.AddTriple(kiyev_name_kiyevName);
            m_graph.AddTriple(kiyev_type_tgn);
            m_graph.AddTriple(leonkrier_activeIn_newyork);
            m_graph.AddTriple(leonkrier_educatedIn_london);
            m_graph.AddTriple(leonkrier_name_leonkrierName);
            m_graph.AddTriple(leonkrier_type_agent);
            m_graph.AddTriple(london_name_londonName);
            m_graph.AddTriple(london_type_tgn);
            m_graph.AddTriple(longbeach_name_longbeachName);
            m_graph.AddTriple(longbeach_type_tgn);
            m_graph.AddTriple(louisikahn_livedIn_longbeach);
            m_graph.AddTriple(louisikahn_livedIn_newyork);
            m_graph.AddTriple(louisikahn_name_louisikahnName);
            m_graph.AddTriple(louisikahn_type_agent);
            m_graph.AddTriple(ludwigmiesvanderrohe_activeIn_moskva);
            m_graph.AddTriple(ludwigmiesvanderrohe_livedIn_moskva);
            m_graph.AddTriple(ludwigmiesvanderrohe_name_ludwigmiesvanderroheName);
            m_graph.AddTriple(ludwigmiesvanderrohe_type_agent);
            m_graph.AddTriple(mariobotta_activeIn_kiyev);
            m_graph.AddTriple(mariobotta_activeIn_moskva);
            m_graph.AddTriple(mariobotta_livedIn_moskva);
            m_graph.AddTriple(mariobotta_name_mariobottaName);
            m_graph.AddTriple(mariobotta_type_agent);
            m_graph.AddTriple(marioromano_activeIn_newyork);
            m_graph.AddTriple(marioromano_name_marioromanoName);
            m_graph.AddTriple(marioromano_type_agent);
            m_graph.AddTriple(masayukikurokawa_livedIn_london);
            m_graph.AddTriple(masayukikurokawa_name_masayukikurokawaName);
            m_graph.AddTriple(masayukikurokawa_type_agent);
            m_graph.AddTriple(morphosissantamonicaca_activeIn_moskva);
            m_graph.AddTriple(morphosissantamonicaca_name_morphosissantamonicacaName);
            m_graph.AddTriple(morphosissantamonicaca_type_agent);
            m_graph.AddTriple(moskva_name_moskvaName);
            m_graph.AddTriple(moskva_type_tgn);
            m_graph.AddTriple(newyork_name_newyorkName);
            m_graph.AddTriple(newyork_type_tgn);
            m_graph.AddTriple(northwales_name_northwalesName);
            m_graph.AddTriple(northwales_type_tgn);
            m_graph.AddTriple(rafaelviñoly_activeIn_newyork);
            m_graph.AddTriple(rafaelviñoly_name_rafaelviñolyName);
            m_graph.AddTriple(rafaelviñoly_type_agent);
            m_graph.AddTriple(raimundabraham_livedIn_moskva);
            m_graph.AddTriple(raimundabraham_name_raimundabrahamName);
            m_graph.AddTriple(raimundabraham_type_agent);
            m_graph.AddTriple(richardmeier_activeIn_edinburgh);
            m_graph.AddTriple(richardmeier_activeIn_london);
            m_graph.AddTriple(richardmeier_educatedIn_london);
            m_graph.AddTriple(richardmeier_name_richardmeierName);
            m_graph.AddTriple(richardmeier_type_agent);
            m_graph.AddTriple(sanfrancisco_name_sanfranciscoName);
            m_graph.AddTriple(sanfrancisco_type_tgn);
            m_graph.AddTriple(santiagocalatrava_livedIn_newyork);
            m_graph.AddTriple(santiagocalatrava_name_santiagocalatravaName);
            m_graph.AddTriple(santiagocalatrava_type_agent);
            m_graph.AddTriple(simonungers_livedIn_boston);
            m_graph.AddTriple(simonungers_livedIn_newyork);
            m_graph.AddTriple(simonungers_name_simonungersName);
            m_graph.AddTriple(simonungers_type_agent);
            m_graph.AddTriple(tadaoando_activeIn_newyork);
            m_graph.AddTriple(tadaoando_livedIn_northwales);
            m_graph.AddTriple(tadaoando_livedIn_wales);
            m_graph.AddTriple(tadaoando_name_tadaoandoName);
            m_graph.AddTriple(tadaoando_type_agent);
            m_graph.AddTriple(thomaskinslow_livedIn_boston);
            m_graph.AddTriple(thomaskinslow_livedIn_newyork);
            m_graph.AddTriple(thomaskinslow_name_thomaskinslowName);
            m_graph.AddTriple(thomaskinslow_type_agent);
            m_graph.AddTriple(thommayne_activeIn_moskva);
            m_graph.AddTriple(thommayne_name_thommayneName);
            m_graph.AddTriple(thommayne_type_agent);
            m_graph.AddTriple(wales_name_walesName);
            m_graph.AddTriple(wales_type_tgn);
            m_graph.AddTriple(zahahadid_activeIn_sanfrancisco);
            m_graph.AddTriple(zahahadid_name_zahahadidName);
            m_graph.AddTriple(zahahadid_type_agent);

            #endregion


            // Only using RdfXml for this example so no need to write/read all files
            // Write RDF
            RDFSerializer.WriteRDF(RDFModelEnums.RDFFormats.RdfXml, m_graph, Server.MapPath("~/").ToString() + "\\" + "tms.rdf");
            //RDFSerializer.WriteRDF(RDFModelEnums.RDFFormats.Turtle, m_graph, Server.MapPath("~/").ToString() + "\\" + "\\tms.ttl");
            //RDFSerializer.WriteRDF(RDFModelEnums.RDFFormats.TriX, m_graph, Server.MapPath("~/").ToString() + "\\" + "\\tms.trix");
            //RDFSerializer.WriteRDF(RDFModelEnums.RDFFormats.NTriples, m_graph, Server.MapPath("~/").ToString() + "\\" + "\\tms.nt");

            // Read RDF 
            RDFGraph tmsRDF = RDFSerializer.ReadRDF(RDFModelEnums.RDFFormats.RdfXml, Server.MapPath("~/").ToString() + "\\tms.rdf");
            //RDFGraph tmsTURTLE = RDFSerializer.ReadRDF(RDFModelEnums.RDFFormats.Turtle, Environment.CurrentDirectory + "\\tms.ttl");
            //RDFGraph tmsTRIX = RDFSerializer.ReadRDF(RDFModelEnums.RDFFormats.TriX, Environment.CurrentDirectory + "\\tms.trix");
            //RDFGraph tmsNT = RDFSerializer.ReadRDF(RDFModelEnums.RDFFormats.NTriples, Environment.CurrentDirectory + "\\tms.nt");

            // Write the RDF into SQL Server.  You have other choices with RDFSharp including RDFMemoryStore
            // You need to create a blank MSSQL database to hold the data.  The database is specified in the 
            // connection string, example: 
            // <add name="RDFConnectionString" connectionString="Data Source=localhost;Initial Catalog=tms-rdf;Integrated Security=True" providerName="System.Data.SqlClient" />

            string m_conn = ConfigurationManager.ConnectionStrings["RDFConnectionString"].ConnectionString;

            RDFSQLServerStore rdf_mssql = new RDFSQLServerStore(m_conn);

            rdf_mssql.MergeGraph(tmsRDF);

            // At this point you can open SQL Server and see a new table called Quadruples with all of the data in the correct format.
            // The first time you run this (successfully), the RdfViewer.aspx page will be empty.
            // Uncomment GetRDF() above and comment LoadRDF() and you should see a datagrid with RDF data.

        }

    }
}