# Netfox.AFX
Netfox.AFX is an experimental implementation of a distributed system that manages evidence 
objects in the style inspired by the AFF4. Netfox.AFX defines an AFX Environment that contains 
all necessary data and services. 
The AFX environment is implemented as a collection of services provided through REST API. 
The specification of the Afx Environment can be found [here](AfxEnvironment.md).

All projects are implemented in C# though it is possible to provide an alternative implementation in other language.

The implemented services are:
* Resource Dictionary - a service used for localization of AFX objects.
* Shell - a client tool for executing operations against the Afx Environment.
* Data Store - a service that can store data and provide an access to data 

# REFERENCES
* Michael Cohen, Simson Garfinkel, Bradley Schatz: Extending the advanced forensic format to accommodate multiple data sources, logical evidence, arbitrary information and forensic workflow, Digital Investigation, Volume 6, Supplement, September 2009.
