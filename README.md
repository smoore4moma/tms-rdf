# tms-rdf
The Museum System RDF 

This is an example of creating and consuming RDF data sourced from The Museum System (TMS).

The example relies heavily on the open source project from Marco De Salvo, RDFSharp.

https://rdfsharp.codeplex.com  or https://github.com/mdesalvo/RDFSharp

The project shows how to create RDF resources using SQL queries against the TMS database, and then store the RDF equivalent in a SQL Server backed RDF store.  You can also see how to create Sparql queries to select the data from the RDF store (database) and show it on a datagrid on a web page.  It should look like this:

<img src='https://github.com/smoore4moma/tms-rdf/blob/master/tms-rdf/img.jpg' />

The idea is not to build a full application or "RDFify" TMS.  The intent is go beyond the theoretical discussions of RDF and apply it to a real-World example, mainly in order to understand the complexities involved, as well as the strengths and limitations of cultural institutions' data and vocabularies. 

<em>Where to start?</em>

This project is mostly intended for developers and technical people.  Still, you get a good idea of what is involved by looking through the code <a href="https://github.com/smoore4moma/tms-rdf/blob/master/tms-rdf/RdfViewer.aspx.cs">here</a>.
