scriptcs-edge
=============

# What is it?

Execute node.js scripts from scriptcs with the power of edge. Script on script!

# Install
```
scriptcs -install ScriptCs.Edge
```

# Sample Usage
```csharp
var edge = Require<EdgePack>();

var func = Edge.Func(
	@"return function(data, callback) {
		callback(null, 'Node js ' + process.version + ' welcomes ' + data);
	}"
);

var msg = (string) func(".NET").Result;
Console.WriteLine(msg);
```


