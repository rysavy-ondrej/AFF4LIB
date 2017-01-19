//  
// Copyright (c) BRNO UNIVERSITY OF TECHNOLOGY. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.  
//
using Afx.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Afx.Services
{
    /// <summary>
    /// This service defines operations to access ResourceDictionary API.
    /// </summary>
    [ServiceContract(Name = "ResourceDirectory")]
    public interface IResourceDirectoryService
    {
        /// <summary>
        /// Gets registered AFF Object and returns JSON representation of its RDF content. To access this operations use "GET /objects/URN" request.
        /// </summary>
        /// <param name="urn">A URN string that specifies AFF object identity.</param>
        /// <returns>A string consisting of JSON representation of its RDF content.</returns>
        [OperationContract]
        [WebGet(UriTemplate = Routing.FindAffObject, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        AfxObject FindObject(string urn);



        /// <summary>
        /// Register a new object. To execute this operation use "POST /objects"
        /// </summary>
        /// <param name="urn"></param>
        /// <param name="rdf"></param>
        /// <returns></returns>
        [OperationContract]        
        [WebInvoke(Method="POST", UriTemplate = Routing.RegisterObject, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool RegisterObject(AfxObject obj);
    }

}
