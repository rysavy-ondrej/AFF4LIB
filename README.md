# AFF4LIB
C# library implementing AFF4 file format

## Basic Concepts

* Universe

* Object

* Volume - is a storage object that must be able to store mupltiple names segments of data. Current specification proposes two volumes - DirectoryVolume and Zip64Volume. DirectoryVolume stores segments within the single directory. Each segment is represented as a file which name is expressed in URN notation. Zip64Volume stores segments in the Zip archive. 

* Stream - stream represents an interface to access data stored in segments. Streams should provide following set of operations: open, seek, write, read, close. Streams should have size attribute to denote the total size of the stream. Following types of stream were proposed: ImageStream, MapStream, HttpStream, EncryptedStream.

* Segment

* Metadata

* Reference

* Resolver

* Links - while objects have unique names it may be difficult to refer to objast because their identifier is random. Link objects can be used to provide for objects short meaningful names.  

# REFERENCES
* Michael Cohen, Simson Garfinkel, Bradley Schatz: Extending the advanced forensic format to accommodate multiple data sources, logical evidence, arbitrary information and forensic workflow, Digital Investigation, Volume 6, Supplement, September 2009.
