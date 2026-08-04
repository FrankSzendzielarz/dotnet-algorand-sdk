









namespace Algorand.Indexer
{
    using Algorand.Indexer.Model;
    using System = global::System;
    using Newtonsoft.Msgpack;


    public partial interface ICommonApi
    {
       /// <summary>Returns 200 if healthy.
       /// </summary>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       System.Threading.Tasks.Task<HealthCheck> makeHealthCheckAsync();

       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       System.Threading.Tasks.Task<HealthCheck> makeHealthCheckAsync(System.Threading.CancellationToken cancellationToken);

    }

    public partial class CommonApi : ICommonApi
    {
       private System.Net.Http.HttpClient _httpClient;
       private System.Lazy<Newtonsoft.Json.JsonSerializerSettings> _settings;

       public CommonApi(System.Net.Http.HttpClient httpClient)
       {
              _httpClient = httpClient;
              _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(CreateSerializerSettings);
       }
       
       private Newtonsoft.Json.JsonSerializerSettings CreateSerializerSettings()
       {
              var settings = new Newtonsoft.Json.JsonSerializerSettings();
              UpdateJsonSerializerSettings(settings);
              return settings;
       }  

       protected Newtonsoft.Json.JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }

       partial void UpdateJsonSerializerSettings(Newtonsoft.Json.JsonSerializerSettings settings);
       partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url);
       partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder);
       partial void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response);

       

       /// <summary>Returns 200 if healthy.
       /// </summary>
       /// <exception cref="ApiException">A server side error occurred.</exception>
       public System.Threading.Tasks.Task<HealthCheck> makeHealthCheckAsync()
       {
              return makeHealthCheckAsync(System.Threading.CancellationToken.None);
       }

       /// <summary>>Returns 200 if healthy.
       /// </summary>
       /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
       /// <exception cref="ApiException<ErrorResponse>">A server side error occurred.</exception>
       public async System.Threading.Tasks.Task<HealthCheck> makeHealthCheckAsync(System.Threading.CancellationToken cancellationToken)
       {
              var urlBuilder_ = new System.Text.StringBuilder();
              urlBuilder_.Append("/health");
              var client_ = _httpClient;
              var disposeClient_ = false;
              try
              {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                     request_.Method = new System.Net.Http.HttpMethod("GET");
                     request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);

                    var url_ = urlBuilder_.ToString();
                    if (url_.StartsWith("/")) { url_ = url_.Remove(0,1); }
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<HealthCheck>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        //Algorand Generator cannot distinguish between response codes
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ErrorResponse>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            throw new ApiException<ErrorResponse>("Error", status_, objectResponse_.Text, headers_, objectResponse_.Object, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }

       }



       protected struct ObjectResponseResult<T>
       {
       public ObjectResponseResult(T responseObject, string responseText)
       {
              this.Object = responseObject;
              this.Text = responseText;
       }

       public T Object { get; }

       public string Text { get; }
       }

       public bool ReadResponseAsString { get; set; }

       protected virtual async System.Threading.Tasks.Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(System.Net.Http.HttpResponseMessage response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Threading.CancellationToken cancellationToken)
       {
       if (response == null || response.Content == null)
       {
              return new ObjectResponseResult<T>(default(T), string.Empty);
       }

       if (ReadResponseAsString)
       {
              string responseText;
                if (response.Content.Headers.ContentType.MediaType == "application/msgpack")
                {
                    using (MessagePackReader reader = new MessagePackReader(await response.Content.ReadAsStreamAsync().ConfigureAwait(false)))
                    {
                        responseText = reader.ReadAsString();
                    }
                }
                else
                {
                    responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
                
              try
              {
                     var typedBody = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseText, JsonSerializerSettings);
                     return new ObjectResponseResult<T>(typedBody, responseText);
              }
              catch (Newtonsoft.Json.JsonException exception)
              {
                     var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
                     throw new ApiException(message, (int)response.StatusCode, responseText, headers, exception);
              }
       }
       else
       {
                 try
                {
                    if (response.Content.Headers.ContentType.MediaType == "application/msgpack")
                    {
                        using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        using (var reader = new MessagePackReader(responseStream))
                        {
                            var serializer = Newtonsoft.Json.JsonSerializer.Create(JsonSerializerSettings);
                            var typedBody = serializer.Deserialize<T>(reader);
                            return new ObjectResponseResult<T>(typedBody, string.Empty);
                        }
                    }
                    else
                    {
                        using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        using (var streamReader = new System.IO.StreamReader(responseStream))
                        using (var jsonTextReader = new Newtonsoft.Json.JsonTextReader(streamReader))
                        {
                            var serializer = Newtonsoft.Json.JsonSerializer.Create(JsonSerializerSettings);
                            var typedBody = serializer.Deserialize<T>(jsonTextReader);
                            return new ObjectResponseResult<T>(typedBody, string.Empty);
                        }
                    }
                                      
                }
              catch (Newtonsoft.Json.JsonException exception)
              {
              var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
              throw new ApiException(message, (int)response.StatusCode, string.Empty, headers, exception);
              }
       }
       }

       private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
       {
              if (value == null)
              {
                     return "";
              }

              if (value is System.Enum)
              {
                     var name = System.Enum.GetName(value.GetType(), value);
                     if (name != null)
                     {
                     var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                     if (field != null)
                     {
                            var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute))
                            as System.Runtime.Serialization.EnumMemberAttribute;
                            if (attribute != null)
                            {
                            return attribute.Value != null ? attribute.Value : name;
                            }
                     }

                     var converted = System.Convert.ToString(System.Convert.ChangeType(value, System.Enum.GetUnderlyingType(value.GetType()), cultureInfo));
                     return converted == null ? string.Empty : converted;
                     }
              }
              else if (value is bool)
              {
                     return System.Convert.ToString((bool)value, cultureInfo).ToLowerInvariant();
              }
              else if (value is byte[])
              {
                     return System.Convert.ToBase64String((byte[])value);
              }
              else if (value.GetType().IsArray)
              {
                     var array = System.Linq.Enumerable.OfType<object>((System.Array)value);
                     return string.Join(",", System.Linq.Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
              }

              var result = System.Convert.ToString(value, cultureInfo);
              return result == null ? "" : result;
              
       }

    }

}
