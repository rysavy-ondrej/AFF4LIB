# AFX environment
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

## URI, URL, and URN
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

# Resource Directory Service
A resource directory service serves for accessing information about known AFX objects.

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
```
GET http://directory.tarzan.org:8465/afx/resourceDirectory/objects/05d7e827

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
```
GET http://directory.tarzan.org:8465/afx/resourceDirectory/objects/a5473593

{
  "id" : "a5473593",
  "type" : "volume",
  "stored" : "https://tx01.tarzan.org:8465/"
}
```
Property ```stored``` contains URL of the host that owns the container. We know
that to access image object we can use its stream interfaces. This interface
is accessible through stream service implemented by the host:
```
GET http://tx01.tarzan.org:8465/afx/stream/05d7e827

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
```
GET http://tx01.tarzan.org:8465/afx/stream/05d7e827/segment/2

<CHUNK-MAP>

<SEGMENT BYTES>
```
The response consists of a chunk map represented as JSON and binary data containing
the segment content.

# Experimental Implementation

## HTTP REST


## OData

# References

* ODATA https://github.com/DevExpress/EF-Core-Security/wiki/How-to-create-a-client-Console-Application-for-the-OData-server-with-the-EF-Core-Security

* DFXML http://www.forensicswiki.org/wiki/Digital_Forensics_XML_Schema
