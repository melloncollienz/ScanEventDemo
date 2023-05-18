To Run the project:

ScanApi is a minimal api.  Start it without debugging.  Then run the ScanEventDemo project.  

Assumptions made

That this is supposed to be a windows service, with a start/stop/event, but I haven't put it into the windows service boilerplate
If it is a windows service, then there should be some logging to the windows event log as well
One other consideration is where the error handling should be.  We can have the data store and the api client handle the exceptions, but how does the "business layer" act when an error happens.  
What rules do we have for duplicates? Should we reject duplicate parcel info when reading from the api?  I haven't written anything to handle this.
What are we doing for API security? is this a public api that is being accessed?
As this is a console app, i've hacked in an Wait() call for my async call.  I know this is wrong/not best practice, but this is a demo console app.
Ideally, I'd want proper enums for the carrier Id and the type.
I would also NOT make everything static in the program cs, but again, this is a demo.


To be honest, I wouldn't put any of this into production.
Here's what I would want for productionised code
Dependency Injection/IOC so that I have an interface for the data store and the api client.
Unit tests for all layers
Database for storing the persisted data
A proper Nlog config + windows event log
An actual windows service boiler plate (if it is a windows service)

After looking at this, I would have to have either a decent conversation with a BA or go over what the actual requirements are here in refinement, depending on what process you are using. 

