#Installer Folder  

in this folder you can register your Component  In StartUp.cs


# Controller 

we use MediatR Package as part of mediator Design Pattern  for all API   

as part of Documentation for a swagger to inform Front-End Developer


We Use Xml document 

```

        /// <summary>
        /// Create Notification
        /// </summary>
        /// <param name="createNotificationCommand"></param>
        /// <returns> Create notification </returns>
        /// <response code="200">if Notification Create Successfully </response>
        /// <response code="400">If Validation Failed</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(typeof(ApiMessage), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]


``` 

Show Status Code and Output Message For Api in swagger

| :exclamation:  This is very important   |


dont forget to add Xml File when You Publish project In IIS 
and if you show Warning in your every Action and need xml document go to build
 section and add 1591 code to ignore warning xml file 