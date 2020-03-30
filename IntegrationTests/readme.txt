How to test local/remote integration tests

local = web api project from current solution
remote = web api already deploy on a machine


LOCAL:
1) set config key 'UseRemote' to false
2) run 'EmployeeApiIntegrationTests2' method 'GetAsync_apiValues'

REMOTE:
1) set config key 'UseRemote' to true
2) deploy webapi to local iis on 'http://integration_api.com'
2) also set config key 'RemoteServerAddress' to 'http://integration_api.com'