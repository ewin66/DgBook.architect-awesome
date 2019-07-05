# 问题:

after updating my project to angular 2 final i had a problem getting  WebApi2 authoriaztion to work with IdentityServer3 and angular2-jwt.  Before everything worked fine when in angular2-jwt configuration the  headername was empty and the token-getter-method returned simply the  token.

After updating an empty headername caused an javascript-error:

```
 EXCEPTION: Failed to execute 'setRequestHeader' on 'XMLHttpRequest': '' is not a valid HTTP header field name.
```

# 回答1:

So first i had to provide a headername, in my root-module angular2-jwt config looked like this:

```js
        provideAuth({
        headerName: 'BearerToken',
        headerPrefix: '',
        tokenName: '',
        tokenGetter: () => {
            return JSON.parse(localStorage.getItem('bearerToken'));
        },
        globalHeaders: [{'Content-Type': 'application/json'}],
        noJwtError: true,
        noTokenScheme: true
    })
```

Still not working. After some research i found the header name should  be 'Authorization' and the tokenName should be 'Bearer'. Ok lets try  like this:

```js
provideAuth({
        headerName: 'Authorization',
        headerPrefix: '',
        tokenName: 'Bearer',
        tokenGetter: () => {
            return JSON.parse(localStorage.getItem('bearerToken'));
        },
        globalHeaders: [{'Content-Type': 'application/json'}],
        noJwtError: true,
        noTokenScheme: true
    })
```

Still my ControllerMethod with the Authorize-Tag was not reached. Ok,  one last try, maybe it works when i add 'Bearer ' manually:

```js
        provideAuth({
        headerName: 'Authorization',
        headerPrefix: '',
        tokenName: 'Bearer',
        tokenGetter: () => {
            var token: string = JSON.parse(localStorage.getItem('bearerToken'));
            return 'Bearer ' + token;                
        },
        globalHeaders: [{'Content-Type': 'application/json'}],
        noJwtError: true,
        noTokenScheme: true
    })
```

and ... bombe surprise ... it worked ;) Playing around a little bit  more i found out that tokenName can be empty or can contain anything  else.