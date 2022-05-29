# WebService Notification

WebService Notification Use For Sending Mail And Message Or call WebService


# Notifier Definition

the project consist of 2 entities  Notifier and Template which  You Can Set Notifier  For Your Service Type 
we have Service as enum in entities 

```
  public enum ServiceType
    {
        Smtp = 1,
        WebService = 2
    }
```


 the setting in serialize json object that define in run time based on your Service here is example of setting  based on type 


 ```
 //Smtp Service

{
  "name": "mohsen kermanifar",
  "userName": "mohsen@gmail.com",
  "password": "mohsen1234",
  "from": "mohsen@gmail.com",
  "host": "smtp.live.com",
  "port": 587,
  "enableSsl": true
}


//wev Service
{
   "id": 10004,
  "name": "Name",
  "url": "https://api.ghasedak.io/v2/sms/send/simple",
  "body": "\r\n{\r\n  \"message\": \"%message%\",\r\n  \"linenumber\": \"9125556644\",\r\n  \"receptor\": \"%to%\"}\r\n",
  "contentType": 2,
  "method": 2,
  "headers": [
    {
      "key": "apikey",
      "value": "d7c1f16922fb96f26b1d665ce1578b86b5c1a5c8bde68341cb1b7d7ee94a5fb9"
    },
    {
      "Key":"Content-Type",
      "Value":"application/x-www-form-urlencoded"
    }
  ]
}

 ```

# Template Definition


you can define message  as html or simple string message  and coustomize your message we set varaiable and tags for Dynamic varaiable and 
when you want define dynamic variable in your Content You must use like this %Name%



```

{
  "title": "Welcome",
  "content": "Welcome mr/ms %Name% to our team",
  "hasHtml": false,
  "tags": [
    "Name"
  ]
}



