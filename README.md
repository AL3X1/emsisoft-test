## Test Task for Emsisoft

### Summary

Create two applications that will generate and process the data using RabbitMQ.
Technology stack
- C#
- .NET 6.0
- SQL Server Express LocalDB
- RabbitMQ

**Application 1 (API)**
- REST API application
- Endpoint POST '/hashes' will generate 40.000 random SHA1 hashes and send them one by one to RabbitMQ queue for further processing
- Endpoint GET '/hashes' will return a number of hashes from the database grouped by day in JSON
  Example of GET response
  {
  "hashes": [
  {
  "date": "2022-06-25",
  "count": 471255631
  },
  {
  "date": "2022-06-26",
  "count": 822365413
  },
  {
  "date": "2022-06-27",
  "count": 1973565411
  }
  ]
  }

**Application 2 (Processor)**
- Background worker application.
- Connect to RabbitMQ queue and process messages in parallel using 4 threads.
- Save message into database table 'hashes' (id, date, sha1).

**Optional features to implement**
- Split generated hashes into batches and send them into RabbitMQ in parallel.
- Retrieve hashes from the database without recalculating data on the fly.


### Architecture

1. #### EmsisoftTest.Api 
    REST API application that contains Hash generation logic. 

    Request processing implemented by next pipeline:
    
    Controller -> Handler -> Command/Query -> Repository -> Database
   
2. #### EmsisoftTest.Data
    Contains Repository, DbContext and migrations
3. #### EmsisoftTest.Domain
    Contains domain logic such as Commands and Queries. In real application it also could contain specific domain related models, enums, extensions, .etc. Domain Commands and Queries can be used within all application modules.
4. #### EmsisoftTest.Infrastructure
    Contains IoC (Autofac) configuration, Command and Query builders.
5. #### EmsisoftTest.Messaging
    Contains Queue wrappers and extensions such as MessageConsumer, Producer, Initializer
6. #### EmsisoftTest.Processor
    Microservice that read hashes from queue and saves them to database

### Deployment

The solution has dockerized setup. Docker configurations are contained in EmsisoftTest/docker directory.
Deployment can be easily used by `make` command.

**Example**: by executing `make help` you will see commands that can be executed for deployment on local machine

```
help:                          help dialog
api:                           build docker container of api
processor:                     build docker container of processor
run-database:                  run database separately (for local debugging)
run-queue:                     run queue separately (for local debugging)
run:                           run services
stop:                          stop services
```

### Testing

Application has basic integration tests and Swagger setup by url: http://localhost:7207/docs/index.html