How to test local/remote integration tests

local = web api project from current solution
remote = web api already deploy on a machine


LOCAL:
1) set config key 'UseRemote' to false
2) run 'EmployeeApiIntegrationTests2' method 'GetAsync_apiValues'
3) 'http://integration_api.com/api/values' response should be
["WebApplication1.Api value1","WebApplication1.Api value2"]

REMOTE:
1) set config key 'UseRemote' to true
2) deploy webapi to local iis on 'http://integration_api.com'
3) also set config key 'RemoteServerAddress' to 'http://integration_api.com'
4) http://localhost:63161/api/values response should be
["local api.WebApplication1.Api value1","local api.WebApplication1.Api value2"]