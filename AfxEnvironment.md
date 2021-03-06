# AFX REST API

# 1 Abstract
AFX is a prototype implementation of distributed forensic data model based on
principles introduced with AFF4.The aim is to provide a simple environment for
storing and accessing data in distributed environment. The environment is designed to be
open for new data types and storage options. Also, the environment does not preclude
any data processing approach.

The approach is based on concepts from Linked Data Platform. Each object is a resource that
is uniquely identified and can be localized using a central or distributed directory service.

As a part of AFX, we developed basic vocabulary for digital forensic artifacts.
This vocabulary is compatible with schema.org initiative that aims at defining
schemas for structured data on the Internet.



# 2 Table of contents
<!-- TOC depthFrom:1 depthTo:3 withLinks:1 updateOnSave:1 orderedList:0 -->

- [1 Abstract](#1-abstract)
- [2 Table of contents](#2-table-of-contents)
- [3 Introduction](#3-introduction)
  - [3.1 URI, URL and URN](#31-uri-url-urn)
  - [3.2 AFX REST API Design](#32-afx-rest-api-design)
- [4 Resource Dictionary](#4-resource-dictionary)
- [5 AFX Objects](#5-afx-objects)
- [6 Interfaces](#6-interfaces)
- [7 Roles](#7-roles)
- [8 Examples](#8-examples)
- [9 Referential Implementation](#9-implementation)
- [References](#x-references)

# 3 Introduction

## 3.1 URI, URL, and URN
A URI identifies a resource either by location, or a name, or both. A URI has two specializations known as URL and URN.
A Uniform Resource Locator (URL) is a subset of the Uniform Resource Identifier (URI) that specifies where an identified resource is available and the mechanism for retrieving it.URL defines how the resource can be obtained. It does not have to be HTTP URL (http://), a URL can also be (ftp://) or (smb://). Example:

```
https://tx001.tarzan.org/services/05d7e827
```

A Uniform Resource Name (URN) is a Uniform Resource Identifier (URI) that uses the URN scheme, and does not imply availability of the identified resource. Example:

```
urn:afx:05d7e827
```
While URN is useful for identification of resources, URL provides also localization.


## 3.2 AFX REST API Design
Representational State Transfer (REST) APIs are service endpoints that support
sets of HTTP operations (methods), which provide create/retrieve/update/delete
access to the service's resources.

Because AFX is aimed as open platform, new REST interfaces will be defined in the future.
In order to maintain interfaces consistent the common guidelines should be followed.
The following link provides Microsoft REST API Guidelines that are adopted for AFX:
https://github.com/Microsoft/api-guidelines/blob/master/Guidelines.md

A REST API request/response pair can be separated into 5 components:

* The request URI, which consists of: ```{URI-scheme} :// {URI-host} / {resource-path} ? {query-string}```
* HTTP request message header fields
* Optional HTTP request message body fields
* HTTP response message header fields
* Optional HTTP response message body fields

### Requests
All requests require to use HTTPS protocol because sensitive information is transmitted.
Request header must contain the following minimum information:
* ```Authorization```: OAuth2 bearer token to secure the request
* ```Content-Type```: specifies the type of the request body, usually set to ```application/json```
* ```Host```: the domain name or IP address of the host providing requested service
Request body is optional and consists of data which format depends on the ```Content-Type```.
Request is sent using HTTP method. Table 1 shows all supported methods.

Method  | Description                                                                                                                | Is Idempotent
------- | -------------------------------------------------------------------------------------------------------------------------- | -------------
GET     | Return the current value of an object                                                                                      | True
PUT     | Replace an object, or create a named object, when applicable                                                               | True
DELETE  | Delete an object                                                                                                           | True
POST    | Create a new object based on the data provided, or submit a command                                                        | False
HEAD    | Return metadata of an object for a GET response. Resources that support the GET method MAY support the HEAD method as well | True
PATCH   | Apply a partial update to an object                                                                                        | False
OPTIONS | Get information about a request; see below for details.                                                                    | True

<small>Table 1: Supported operations of the REST API</small>

### Responses
If response is successful, then it contains ```200 OK``` code and the following
fields:
* ```Content-Length```: defines the length of the message body.
* ```Content-Type```: specifies the tpye of the response body.

A list of error codes can be lengthy depending on the particular service.  
The most common error codes are:
* ```400 Bad Request``` - A required parameter was not provided or the request was incorrect/incomplete.
* ```403 Forbidden``` - Server failed to authenticate the request.
* ```404 Not Found``` - The specified resource does not exist.
* ```405 Method not Allowed``` - The resource doesn't support the specified HTTP verb (GET, PUT, POST, DELETE).

### New Header Fields
Considerations for new header fields is described in RFC7231, section 8.3.1.
While new headers should be avoided there may be cases when a new header definition
may be suitable. For instance, streams are read by segments, but the segment itself
consists of chunks. Because chunks can be compressed, a chunk map needs to be retrieved
with the segment to understand the segment organization.
The segment itself is sent as binary data (```application/octet-stream```). To
transfer a chunk map we have several options:
* Employ an existing header for chunk representation, e.g., ```Content-Range``` (https://tools.ietf.org/html/rfc7233)
* Send chunk map in a new header, e.g., ```Content-Map```
* Send chunk map and segment binary data  as ```multipart/mixed```
* Prepend the segment binary data with an encoded chunk map


# 4 Resource Directory 
A resource directory service maintains a set of AFX Objects visible in the Afx Environment. 
Each object is identified by its unique identifier which is used as a key in the resource directory. 
A full object description is stored in resource directory and is provided to clients using JSON-LD format. 

## 4.1 Assumptions and Goals
The Afx Environment contains a collection of Afx Objects. Each object represents a resource that can 
be uniquely identified and accessed using a well-defined interface.

Each object should be registered in a Resource Directory to be accessible within the Afx Environment. 
While it is possible to create an object and share object's description among services, only registered objects
are visible in Afx Environment. Objects invisible in the environment are usualy used only as temporally 
or intermediate objects.

The Resource Directory can be deployed as a single service for a small environment or as a cluster of distributed 
directories for large or high-performance environments. 
The way in which the resource directory service is deployed SHOULD be transparent to clients. 

## 4.2 Operations

The Resource Dictionary exposes the following public operations:

| Operation | URI                                 | Description                                                     |
| --------- | ----------------------------------- | --------------------------------------------------------------- |
| GET       | /afx/resourceDirectory/objects/{id} | Finds an object specified by its {id}.                          |
| POST      | /afx/resourceDirectory/objects      | Inserts a new object sent as JSON-LD in payload of the request. |
| DELETE    | /afx/resourceDirectory/objects/{id} | Removes an object specified by its {id} from the dictionary.    |

### Find Object
Find Object operation attempts to find a registered Afx Object in the resource directory.

```http
GET /afx/resourceDirectory/objects/{id} HTTP/1.1
Accept: application/json
Authorization: Bearer {bearerToken}
Host: {directoryHost}


HTTP/1.1 200 OK
Content-Length: {length}
Content-Type: application/json

{afxObject}
```
* ```id``` is object id, usually represented using its URN
* ```bearerToken``` valid token obtained from the authorization service
* ```directoryHost``` domain name or ip address of a host providing resource directory
* ```length``` length of the answer, i.e., length of afxObject JSON-LD representation.
* ```afxObject``` Json-LD representation of AfxObject.

### Register Object
Object registration inserts a new Afx Object description in to the Resource Directory.

```http
POST /afx/resourceDirectory/objects HTTP/1.1
Authorization: Bearer {bearerToken}
Host: {directoryHost}

{afxObject}


HTTP/1.1 200 OK
???
```

### Deregister Object
Removes the specified object from the resource dictionary. The object still exists but 
it is not visible in the environment. 

```http
DELETE /afx/resourceDirectory/objects/{id} HTTP/1.1
Accept: application/json
Authorization: Bearer {bearerToken}
Host: {directoryHost}


HTTP/1.1 200 OK
Content-Length: {length}
Content-Type: application/json

{afxObject}
```
* ```id``` is object id, usually represented using its URN
* ```bearerToken``` valid token obtained from the authorization service
* ```directoryHost``` domain name or ip address of a host providing resource directory
* ```length``` length of the answer, i.e., length of afxObject JSON-LD representation.
* ```afxObject``` Json-LD representation of AfxObject.

# 5 AFX Objects

Standard attributes of all objects are as following:

| Property         | Type        | Description                     |
| ---------------- | ----------- | -------------------------------|
| stored           | URI         | Link to an object in which the current object is stored. |
| type             | Text        | A type of the current object. See known types bellow. |
| interface        | Text        | An interface supported by the object. |
| timestamp        | DateTime    | Timestamp indicating when the object was created. |


AFX recognizes the following general object types:

| Type Name   | Description                                                                                        |
| ----------- | ---------------------------------------------------------------------------------------------------|
| volume      | A volume provides storage container for other data objects.                                        |
| image       | Image is a type that stores a single forensic data set.                                            |
| map         | Map is type that enables to expose stream interface for data distributed in a different locations. |
| pcap        | This object represents a packet capture file.                                                      |
| logfile     | This object describes a file containing logs.                                                      |
| nfcap       | This object represents a file containting flow records and created by nfdump tool.                 |
| link        | Link is an object type that enables to create aliases.                                             |
| identity    | Identity is a type of objects that represents verifiable entity by X.509 certificate.              |
| case        | Case is a collection of other objects that are part of a single investigation.                     |

Each object type can have more specific subtypes and attributes that provide addtional information.

## 5.1 Volume Objects
Volumes are containers that host other objects. They also define the low level access
to objects stored in the container. For Zip container, the access means to use functions of ZIP utility to obtain individual segments stored in the ZIP archive.

| Property           | Type        | Description                           |
| ------------------ | ----------- | --------------------------------------|
| description        | Text        | Description of the current container. |

## 5.2 Image Objects

There are three image subtypes:

* PageImage
* BlockImage
* SegmentImage

### Page Images
Page Images are a collection of 512-byte pages optimized for random read and write operations.


### Block Images
Block Images are comprised of blocks of data. Blocks do not have to be of the same size.
Blocks can be up to 32 MB. 

| Property               | Type         | Description                        |
| ---------------------- | ------------ | -----------------------------------|
| blockSize              | Integer      | Size of the block.                 |
| size                   | Integer      | Total size of the object if known. |

### Segment Image Objects
Segment Images are comprised of segments that are identified within the image by unique IDs. 
Each segment is further divided into chunks. Chunks can be compressed to reduce the size of the segment. 
Splitting the image into segments enables to improve the performance by uploading multiple segments in parallel.

| Property               | Type                 | Description                            |
| ---------------------- | -------------------- | ---------------------------------------|
| chunkSize              | Integer              |                                        |
| chunksPerSegment       | Integer              |                                        |
| compression            | Text                 | Compression method applied to chunks.  |
| size                   | Integer              | Total size of the object if known.     |

## 5.3 Map Objects

## 5.4 Capture Objects



## 5.5 Nfcap Objects
| Property         | Type    | Description                     |
| ---------------------- | ----------------- | -------------------------------|
| format          | Enum              |  nfdump has four fixed output formats: raw, line, long and extended. |                               |

## 5.6 Link Objects

## 5.7 Identity Objects

## 5.8 Case Objects 


# 6 Interfaces
Interface is a mean how a client can access the content of an AFX Object.



| Interface Name   | Description                                                                                        |
| ----------- | ---------------------------------------------------------------------------------------------------|
| Container | The container interface provides access to the content of various containers. |
| Stream | The Stream interface provides storage for entities, such as binary files and text files. |
| Queue | The Queue interface provides reliable, persistent messaging within and between services. |
| Table | The Table interface provides access to a structured storage in the form of tables. |
| Collection | The Collection interface provides access to a container of JSON documents and associated JavaScript application logic, i.e. stored procedures, triggers and user-defined functions. |
| Document |  The Document interface provides access to user-defined content in JSON format. Documents are stored within collections. |

## 6.1 Container Interface


## 6.2 Stream Interface
Stream interface is a basic method of accessing data organized in blocks. Data are read and written 
at the block level. Depending on the underlying object the size of the block varies. 

Streams are accessed by the following operations:

| Operation | URI     | Description                     |
| ---------------------- | ----------------- | -------------------------------|
| GET | /stream | List of stream available on the specific host.|
| GET | /stream/{streamId} | Reads information about the stream itself and available block. |
| GET | /stream/{streamId}/block/{blockId} | Reads the specified block. |
| POST | /stream/{streamId}/block/{blockId} | Writes the block at the specified position |


### Stream Information
Reads information about the stream itself and available segments.

```http
GET /afx/stream/{id} HTTP/1.1
Accept: application/json
Authorization: Bearer {bearerToken}
Host: {host}


HTTP/1.1 200 OK
Content-Length: {length}
Content-Type: application/json

{afxStreamObject}
```
* ```id``` is object id that provides the stream interface
* ```bearerToken``` valid token obtained from the authorization service
* ```directoryHost``` domain name or ip address of a host implementing  the service
* ```length``` length of the answer, i.e., length of afxObject JSON-LD representation.
* ```afxObject``` Json-LD representation of the stream backing object.

### Stream Read
Reads a single block from the specified stream.
```http
GET /afx/stream/{streamId}/block/{blockId} HTTP/1.1
Accept: application/octet-stream
Authorization: Bearer {bearerToken}
Host: {directoryHost}


HTTP/1.1 200 OK
Content-Length: {length}
Content-Type: application/octet-stream
Content-Map: {chunkMap}

{data}
```
* ```streamId``` is object id that provides the stream interface
* ```blockId``` is the identification of the block. Blocks are numbered from 0.
* ```bearerToken``` valid token obtained from the authorization service
* ```directoryHost``` domain name or ip address of a host providing resource directory
* ```length``` length of the answer, i.e., length of afxObject JSON-LD representation.
* ```chunkMap``` json representation of a chunk map. For example: ```{ "chunks" : [ 0, 1024, 2048, 3072 ]  }```
* ```data``` binary data of the block.

### Stream Write
Writes a single block to the specified stream. 

```http
POST /stream/{streamId}/block/{blockId}  HTTP/1.1
Authorization: Bearer {bearerToken}
Host: {directoryHost}
Content-Type: application/octet-stream
Content-Map: {chunkMap}

{data}
```
* ```streamId``` is object id that provides the stream interface
* ```blockId``` is the identification of the block. Block are numbered from 0.
* ```bearerToken``` valid token obtained from the authorization service
* ```directoryHost``` domain name or ip address of a host providing resource directory
* ```length``` length of the answer, i.e., length of afxObject JSON-LD representation.
* ```chunkMap``` json representation of a chunk map. For example: ```{ "chunks" : [ 0, 1024, 2048, 3072 ]  }```
* ```data``` binary data of the block.

### Chunk Map
The chunk map specifies how binary data are organized in chunks. This mapping is important
as chunks can be compressed. JSON representation contains an array of chunks offsets
within the segment.
The following is an example of a segment comprising of four chunks of the same length:

```
{
    "chunks" : [ 0, 1024, 2048, 3072 ]  
}
```

## 6.3 Queue Interface


## 6.4 Table Interface


## 6.5 Collection Interface

## 6.6 Document Interface


# 7 Roles
Nodes in Afx Environment appear in some defined roles. 
The following roles are recognized:

| Role Name   | Description                                                                                        |
| ----------- | ---------------------------------------------------------------------------------------------------|
| Directory role | Directory role provides information about Afx Object through Directory Service interface.  
                   There may be more than a single node in this role. In this case the directory is distributed.   |
| Storage role | Storage role provides services for accessing data of various type. It provides one or more  
                 data access services.                     |
| Worker role | Worker role performs data manipulation wihtin the Afx Environment.  |
| Producer role | Producer role provides access to external data to the Afx Environment.  |
| Consumer role | Consumer role uses data available in the Afx Environment.  |
| Authorization role | Authorization role provides the authorization service for Afx Environment that supports OAuth 2.0 framework. |

# 8 Examples
Following examples illustrate the way in which different objects can be accessed.
JSON-LD is used for representing schema of the AFX Objects.

## 8.1 Access data in Image object
This example shows how to read data from the image object ```afx:05d7e827```.
First we need to localize the container of the image. Thus we ask directory service
to provide us information on the image object:

```http
GET https://directory.tarzan.org:8465/afx/resourceDirectory/objects/05d7e827  HTTP/1.1
Accept: application/json


HTTP/1.1 200 OK
Content-Length: 91
Content-Type: application/json

{
  "id" : "05d7e827",
  "type" : "segmentImage",
  "interface" : "stream",
  "chunkSize" : 2048,
  "chunksPerSegment" : 16,
  "compression" : "identity",
  "size" : 132456,
  "segmentCount" : 4
  "stored" : "urn:afx:a5473593"
}
```

The response is JSON-LD object declaration. Except information on the image structure
we get ```stored``` property that specifies the image's container object. We can use
this object to get URL of the host:

```http
GET https://directory.tarzan.org:8465/afx/resourceDirectory/objects/a5473593 HTTP/1.1
Accept: application/json


HTTP/1.1 200 OK
Content-Length: 91
Content-Type: application/json

{
  "id" : "a5473593",
  "type" : "volume",
  "stored" : "https://tx01.tarzan.org:8465/"
}
```

Property ```stored``` contains URL of the host that owns the container. We know
that to access image object we can use its stream interfaces. This interface
is accessible through stream service implemented by the host:

```http
GET https://tx01.tarzan.org:8465/afx/stream/05d7e827 HTTTP/1.1
Accept: application/json


HTTP/1.1 200 OK
Content-Length: 223
Cntent-Type: application/json

{
  "id" : "05d7e827",
  "type" : "segmentImage",
  "interface" : "stream",
  "chunkSize" : 2048,
  "chunksPerSegment" : 16,
  "compression" : "identity",
  "size" : 132456,
  "segmentCount" : 4,
  "stored" : "urn:afx:a5473593"
}
```

Note that to access image using stream interface we still use image object URN.
It is because Stream is just an interface implemented as web service to access
the object.

To read the third segment the following GET command is issued:

```http
GET http://tx01.tarzan.org:8465/afx/stream/05d7e827/block/2 HTTTP/1.1
Accept: application/octet-stream


HTTP/1.1 200 OK
Content-Length: <SEGMENT-LENGTH>
Cntent-Type: application/octet-stream
Content-Map: { "chunks": [0, 188, 1034, 1052] }

<SEGMENT-BYTES>
```

The response consists of a chunk map represented as JSON and binary data containing
the segment content.

# 9 Experimental Implementation

## HTTP REST


## OData

# References

* [How to create a client Console Application for the OData server with the EF Core Security](https://github.com/DevExpress/EF-Core-Security/wiki/How-to-create-a-client-Console-Application-for-the-OData-server-with-the-EF-Core-Security)

* [Dgitial Forensics XML Schema](http://www.forensicswiki.org/wiki/Digital_Forensics_XML_Schema)

* [Microsoft Azure: Operations on Blobs](https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/operations-on-blobs)
