# AFX environment

## 1 Abstract
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



## 2 Table of contents
<!-- TOC depthFrom:1 depthTo:3 withLinks:1 updateOnSave:1 orderedList:0 -->

- [1 Abstract](#1-abstract)
- [2 Table of contents](#2-table-of-contents)
- [3 Introduction](#3-introduction)
  - [3.1 URI, URL and URN](#31-uri-url-urn)
  - [3.2 AFX REST API Design](#32-afx-rest-api-design)
- [4 Resource Dictionary](#4-resource-dictionary)
- [5 AFX Objects]()
- [6 Interfaces]()
- [7 Examples]()
- [8 Referential Implementation]()
- [References]()

## 3 Introduction

### 3.1 URI, URL, and URN
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


### 3.2 AFX REST API Design
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

<small>Table 1</small>

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


## Resource Directory Service
A resource directory service serves for accessing information about known AFX objects.

### Find Object
Find Object operation attempts to find a registered Afx Object in the resource directory.

```http
> GET /afx/resourceDirectory/objects/{id} HTTP/1.1
> Authorization: Bearer {bearerToken}
> Host: {directoryHost}
>
< HTTP/1.1 200 OK
< Content-Length: {length}
< Content-Type: application/json
<
< {afxObject}
```
* ```id``` is object id, usually represented using its URN
* ```bearerToken``` valid token obtained from the authorization service
* ```directoryHost``` domain name or ip address of a host providing resource directory
* ```length``` length of the answer, i.e., length of afxObject JSON-LD representation.
* ```afxObject``` Json-LD representation of AfxObject.

### Register Object


## Interface
The interface of the resource directory service is defined as follows:
```
public interface IResourceDirectoryService
{
        AfxObject FindObject(string urn);
        bool RegisterObject(AfxObject obj);
}
```




# AFX Objects

Standard attributes of all objects are as following:

| Property         | Type     | Description                     |
| ---------------------- | ----------------- | -------------------------------|
| stored             | URI                 | Link to an object in which the current object is stored. |
| type               | Text                 | A type of the current object. See known types bellow. |
| interface          | Text                 | An interface supported by the object. |
| timestamp          | DateTime                 | Timestamp indicating when the object was created. |


AFX recognizes the following general object types:

| Type Name         | Description                     |
| -------------------- | -------------------------------|
| volume  | A volume provides storage container for other data objects. |
| image  | Image is a type that stores a single forensic data set. |
| map    | Map is type that enables to expose stream interface for data distributed in a different locations. |
| database  | Database is an object type that represents a database storage. |
| link  | Link is an object type that enables to create aliases. |
| identity  | Identity is a type of objects that represents verifiable entity by X.509 certificate. |


## Volume Objects
Volumes are containers that host other objects. They also define the low level access
to objects stored in the container. For Zip container, the access means to use functions of ZIP utility to obtain individual segments stored in the ZIP archive.

| Property       | Type     | Description                     |
| ---------------------- | ----------------- | -------------------------------|
| description        | Text                 | Description of the current container. |

## Image Objects

There are three image subtypes:

* BlockImage
* PageImage
* AppendImage

| Property         | Type    | Description                     |
| ---------------------- | ----------------- | -------------------------------|
| chunkSize          | Integer              |                                 |
| chunksPerSegment   | Integer              |                                 |
| compression        | Text                 | Compression method applied to chunks. |
| size               | Integer              | Total size of the object if known. |




https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/operations-on-blobs
## Map Objects

## Database Objects

## Link Objects

## Identity Objects

# Interfaces
Interface is a mean how a client can access content of AFX Object.
The defined interfaces are:

* Container - The container interface provides access to the content of various containers.
* Stream - The Stream interface provides storage for entities, such as binary files and text files.
* Queue - The Queue interface provides reliable, persistent messaging within and between services.
* Table - The Table interface provides access to a structured storage in the form of tables.
* Collection - The Collection interface provides access to a container of JSON documents and associated JavaScript application logic, i.e. stored procedures, triggers and user-defined functions.
* Document -  The Document interface provides access to user-defined content in JSON format. Documents are stored within collections.

## Stream
Stream interface is a basic method of accessing data organized in segments that can be read
sequential.

### Get Stream information
Reads information about the stream itself and available segments.
```http
GET /afx/stream/{id} HTTP/1.1
Accept: application/json
Authorization: Bearer {bearerToken}
Host: {directoryHost}


HTTP/1.1 200 OK
Content-Length: {length}
Content-Type: application/json

{afxStreamObject}
```
* ```id``` is object id that provides the stream interface
* ```bearerToken``` valid token obtained from the authorization service
* ```directoryHost``` domain name or ip address of a host providing resource directory
* ```length``` length of the answer, i.e., length of afxObject JSON-LD representation.
* ```afxObject``` Json-LD representation of the stream backing object.

### Stream Read
Reads a single segment from the specified stream.
```http
GET /afx/stream/{streamId}/segment/{segmentId} HTTP/1.1
Accept: application/octet-stream
Authorization: Bearer {bearerToken}
Host: {directoryHost}


HTTP/1.1 200 OK
Content-Length: {length}
Content-Type: application/octet-stream
x-afx-chunk-map: {chunkMap}

{segmentData}
```
* ```streamId``` is object id that provides the stream interface
* ```segmentId``` is the identification of the segment. Segments are numbered from 0.
* ```bearerToken``` valid token obtained from the authorization service
* ```directoryHost``` domain name or ip address of a host providing resource directory
* ```length``` length of the answer, i.e., length of afxObject JSON-LD representation.
* ```chunkMap``` json representation of a chunk map. For example: ```{ "chunks" : [ 0, 1024, 2048, 3072 ]  }```
* ```segmentData``` binary data containing the segment.

### Stream Write



Streams are exposed by StreamService API:

| Operation | URI     | Description                     |
| ---------------------- | ----------------- | -------------------------------|
| GET | /stream | List of stream available on the specific host.|
| GET | /stream/{streamId} | Reads information about the stream itself and available segments. |
| GET | /stream/{streamId}/segment/{segmentId} | Reads the specified segment. |
| POST | /stream/{streamId}/segment/{segmentId} | Writes the segment at the specified position |

StreamService API is defined as IStreamService as follows:
```
interface IStreamService
{
  AfxObject Stream(string streamId);
  Segment Read(string streamId, long segmentIndex);
  void Write(string streamId, long segmentIndex, Segment segment);
}
```

A Response consists of a chunk map and a binary content of the segment.
The chunk map specifies how binary data are organized in chunks. This mapping is important
as chunks can be compressed. JSON representation contains an array of chunks offsets
within the segment.
The following is an example of a segment comprising of four chunks of the same length:
```
{
    "chunks" : [ 0, 1024, 2048, 3072 ]  
}
```


# Examples
Following examples illustrates the way in which different objects can be accessed.
Json-LD is used for representing schema of the AFX Objects.
# Access data in Image object
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
  "type" : "image",
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
  "type" : "image",
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
GET http://tx01.tarzan.org:8465/afx/stream/05d7e827/segment/2 HTTTP/1.1
Accept: application/octet-stream


HTTP/1.1 200 OK
Content-Length: <SEGMENT-LENGTH>
Cntent-Type: application/octet-stream
x-afx-chunk-map: { "chunks": [0, 188, 1034, 1052] }

<SEGMENT-BYTES>
```
The response consists of a chunk map represented as JSON and binary data containing
the segment content.

# Experimental Implementation

## HTTP REST


## OData

# References

* ODATA https://github.com/DevExpress/EF-Core-Security/wiki/How-to-create-a-client-Console-Application-for-the-OData-server-with-the-EF-Core-Security

* DFXML http://www.forensicswiki.org/wiki/Digital_Forensics_XML_Schema
