# AFX environment





# Resource Directory Service

# AFX Objects

Standards attributes of all objects are as following:

| Attribute Name         | Default Value     | Description                     |
| ---------------------- | ----------------- | -------------------------------|
| afx:stored             | -                 | Link to an object in which the current object is stored. |
| afx:type               | -                 | A type of the current object. See known types bellow. |
| afx:interface          | -                 | An interface usable to access the current object. See possible interfaces. |
| afx:timestamp          | -                 | Timestamp indicating when the object was created. |


AFX recognizes the following object types:

| Type Name         | Description                     |
| -------------------- | -------------------------------|
| volume  | A volume provides storage container for other data objects. |
| image  | Image is a type that stores a single read-only forensic data set. |
| map    | Map is type that enables to expose stream interface for data distributed in a different locations. |
| table  | Table is an object type that represents a database table. |
| link  | Link is an object type that enables to create aliases. |
| identity  | Identity is a type of objects that represents verifiable entity by X.509 certificate. |

## Container Objects
Containers are objects that may contain other objects. They also define the low level access
to objects stored in the container. For Zip container, the access means to use functions of ZIP utility to obtain individual segments stored in the ZIP archive.

| Attribute Name         | Default Value     | Description                     |
| ---------------------- | ----------------- | -------------------------------|
| afx:description        | -                 | Description of the current container. |


## Image Objects

| Attribute Name         | Default Value     | Description                     |
| ---------------------- | ----------------- | -------------------------------|
| afx:chunk_size         | -                 |                                 |
| afx:chunks_per_segment | -                 |                                 |
| afx:compression        | identity          | Compression method applied to chunks. |
| afx:size               | -                 | Total size of the object if known. |


## Table Objects
Table object is designed to provide access to tabular data possibly stored in the database.
The table thus contains items that represents rows. Table itself have definition of columns.
## Link Objects


## Identity Objects

# Interfaces
Interface is a mean how a client can access content of AFX Object.

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
Stream offers Read operation:
```
interface IStreamService
{
  AfxObject Stream(string streamId);
  Segment Read(string streamId, long segmentIndex);
  void Write(string streamId, long segmentIndex, Segment segment);
}
```

## Crud
Another supported interface realizes create, read, update, delete set of operations
that are typical for database access.

# Examples

Image object ```afx://05d7e827```, which is stored in container ```afx://a5473593```.
```
afx://05d7e827
  afx:type=image
  afx:interface=stream
  afx:stored=afx://a5473593
```
To read the image we first need to get URI for stream service:
```
GET http://directory.tarzan.org:8465/AfxResourceDirectory/objects/a5473593

afx://a5473593
  afx:type=volume
  afx:stored=htpp://tx01.tarzan.org:8465/
```
Then, we can access image using stream interface provided by the node that
hosts the volume:
```
GET http://tx01.tarzan.org:8465/afx/stream/05d7e827

afx://05d7e827
  afx:type=image
  afx:interface=stream
  afx:segment_size=2048
  afx:chunks_per_segment=16
  afx:compression=identity
  afx:size=132456
  afx:segment_count=4
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
# Experimental Implementation

## HTTP REST


## OData


https://github.com/DevExpress/EF-Core-Security/wiki/How-to-create-a-client-Console-Application-for-the-OData-server-with-the-EF-Core-Security
