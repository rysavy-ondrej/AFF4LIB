//  
// Copyright (c) BRNO UNIVERSITY OF TECHNOLOGY. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.  
//
using Afx.Model;
using Afx.Services;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using JsonLD;
namespace ResourceDirectory
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
                 ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true)]
    class ResourceDirectory : IResourceDirectoryService
    {
        Dictionary<string, AfxObject> m_resourceDictionary = new Dictionary<string, AfxObject>();


        public AfxObject FindAffObject(string urn)
        {
            if (m_resourceDictionary.TryGetValue(urn, out AfxObject rdf))
            {
                return rdf;
            }
            else
            {
                return null;
            }                        
        }

        public bool RegisterObject(AfxObject obj)
        {
            if (!m_resourceDictionary.TryGetValue(obj.Id, out AfxObject rdf))
            {
                m_resourceDictionary.Add(obj.Id, obj);
                Console.WriteLine($"New object {obj.Id} registered.");
                Console.WriteLine($"Total number of known objects is {m_resourceDictionary.Count}.");
                return true;
            }
            else
                return false;
        }


        public ResourceDirectory()
        {
            m_resourceDictionary.Add("urn:aff:1", new AfxObject
            {
                Id = "urn:aff:1",
                Attributes = new Dictionary<string, string>
                {
                    [AfxAttributes.Type] = "image",
                    [AfxAttributes.Interface] = "stream",
                    [AfxAttributes.Timestamp] = "2016-12-24T16:54.125",
                    [AfxAttributes.ChunkSize] = "32768",
                    [AfxAttributes.ChunksInSegment] = "1024",
                    [AfxAttributes.Compression] = "gzip"
                }
            });
        }
    }
}
