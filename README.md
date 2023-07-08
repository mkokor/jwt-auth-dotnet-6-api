# JSON Web Token Authentication and Authorization API
<p align="justify">
</p>

## JSON Web Token
Example of JWT is shown bellow.

```bash
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c
```

<p align="justify">
Based on the example above, it can be concluded that a JWT consists of three main parts that are separated by a dot character. The content of each part is encoded    in Base64URL format. Each Base64URL code represents a JSON object, and that is precisely why the descriptive attribute "JSON" is present in the name of this type     of token. The term "Web" indicates that this type of token is used exclusively on the internet.
Therefore, a JWT contains three JSON objects (encoded in Base64URL format) within it. Each of these objects carries specific information used to determine the validity and integrity of the JWT, as well as the identity of the user for whom the JWT is issued.
</p>

### Header
<p align="justify">
The first part of the JWT is called the "header". The header typically contains two attributes: "alg" and "typ". The "alg" attribute refers to the algorithm used to sign the JWT, while the "typ" attribute indicates the token type (usually with the value "JWT", although there are exceptions). An example of the header is shown in the code snippet below.
</p>

```json
{
  "alg": "HS256",
  "typ": "JWT"
}
```

As mentioned earlier, the given JSON object is converted into Base64URL encoding and used in the JWT.
