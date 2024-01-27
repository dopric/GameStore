# Auth0
## Setup
define api in applications\apis create api
add identifier https://gamestoreapi.com  // not so important
setup permisions
add permision games:read games:write
## User and Roles
user management create user
create role admin
assign role to user

actions-> library-> build custom
crate action: Add Roles -> Trigger: Login post login
```js
exports.onExecutePostLogin = async(event, api) => {
    const roleClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    if(event.authorization){
        api.idToken.setCustomClaim(roleClaim, event.authorization.roles);
        api.accessToken.setCustomClaim(roleClaim, event.authorization.roles);
    }
```

// Deploy Action
// goto flows -> login -> custom tab-> drag over add roles
## Integration
Add Application -> create application -> game store client 
settings -> application URIs -> http://localhost:4200
allowed callback URLs -> http://localhost:4200/authentication/login-callback
allowed logout URLs -> http://localhost:4200

