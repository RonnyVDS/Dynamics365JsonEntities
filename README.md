# Dynamics365 JSON Entities

Convert JSON files to Entity Objects.

Use this for writing unit test when working with CRM, XRM or Dynamics 365


Use 'Redirecting Assembly Versions' when you want to use a differend veriosn of the Dynamics 365 dlls.
```xml
<runtime>
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
      <dependentAssembly>
        <assemblyIdentity name="Microsoft.Xrm.Sdk" publicKeyToken="31bf3856ad364e35" culture="neutral" />
        <bindingRedirect oldVersion="0.0.0.0-9.0.0.0" newVersion="8.2.0.0" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="Newtonsoft.Json" publicKeyToken="30ad4fe6b2a6aeed" culture="neutral" />
        <bindingRedirect oldVersion="0.0.0.0-12.0.0.0" newVersion="10.0.0.0" />
      </dependentAssembly>
    </assemblyBinding>
  </runtime>
  ```