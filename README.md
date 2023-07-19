# JSON Web Token Authentication and Authorization API
<p align="justify">
This repository provides the source code for a .NET 6 Web API implementing JWT-based authentication and authorization. In addition to the access token (JWT), the server also generates a refresh token, which is securely stored in an HttpOnly cookie. Moreover, the code includes a method to read and process claims from the JWT, enabling the application to extract relevant user information and permissions during the authentication process. On localhost startup, Swagger UI of an API can be found at <a href="https://localhost:5000/swagger/index.html">https://localhost:5000/swagger/index.html</a>.
</p>


## JSON Web Token
<p align="justify">
Example of JWT is shown bellow.
</p>

```bash
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c
```

<p align="justify">
Based on the example above, it can be concluded that a JWT consists of three main parts that are separated by a dot character. The content of each part is encoded  in Base64URL format. First two Base64URL codes represent a JSON objects, and that is precisely why the descriptive attribute "JSON" is present in the name of this type     of token. The term "Web" indicates that this type of token is used exclusively on the internet.
Therefore, a JWT contains three JSON objects (encoded in Base64URL format) within it. Each of these objects carries specific information used to determine the validity and integrity of the JWT, as well as the identity of the user for whom the JWT is issued.
</p>


### Header
<p align="justify">
The first part of the JWT is called the header. The header typically contains two attributes: "alg" and "typ". The "alg" attribute refers to the algorithm used to sign the JWT, while the "typ" attribute indicates the token type (usually with the value "JWT", although there are exceptions). An example of the header is shown in the code snippet below.
</p>

```json
{
  "alg": "HS256",
  "typ": "JWT"
}
```

<p align="justify">
As mentioned earlier, the given JSON object is converted into Base64URL encoding and used in the JWT.
</p>


### Payload
<p align="justify">
The second part of JWT is called the payload. The payload of a JWT contains the claims, which represent information about the entity (typically the user) for whom the JWT is issued, along with some additional data. There are two types of claims: registered claims and custom claims.
</p>
<p align="justify">
Registered claims are claims defined in the JWT specification and are used to convey common information about the JWT. Examples of registered claims include:
</p>
<ul>
  <li>iss (Issuer) – JWT issuer identifier;</li>
  <li>sub (Subject) – JWT subject identifier;</li>
  <li>exp (Expiration) – JWT expiration time;</li>
  <li>aud (Audience) – recipients for whom JWT is intended.</li>
</ul>
<p align="justify">
Custom claims are not universally accepted by standards but are defined according to the specific needs of an application. These claims can be either public or private. Public custom claims typically use widely accepted names (and standards for them), while private custom claims use names specific to the application in which they are used. Therefore, the term "privacy" in the context of custom claims does not imply visibility only to the JWT holder but rather the adherence of those claims to standard conventions. An example of a public custom claim would be "name" (representing the user's name for whom the JWT is issued), and private custom claims could be "user_id" or any other application-specific information.
</p>
<p align="justify">
In the code snippet below, an example JSON object representing the payload part of a JWT is visible:
</p>

```json
{
  "iss": "my-application",
  "sub": "user1",
  "exp": 1673925623,
  "name": "John Doe",
  "role": "admin"
}
```


### Digital Signature
<p align="justify">The last part of the JWT is called the digital signature. The digital signature is used to verify the integrity and authenticity of the JWT. It is created using an algorithm specified in the JWT header (as mentioned earlier). Such an algorithm generates a digital signature based on the content of the header and payload of the JWT (both encoded in Base64URL format) and a specific key, which can be a private key or a secret key (depending on the algorithm used). Unlike the previous two parts of the JWT, the digital signature is not an encoded JSON object but the result of a mathematical operation.
</p>
<p align="justify">
When an application receives a JWT, it calculates the value of the digital signature based on the content of the header and payload, as well as the key (private or secret). If the calculated value matches the one present in the received JWT, it means that the JWT was indeed issued by that application and has not been modified in the meantime.
</p>
