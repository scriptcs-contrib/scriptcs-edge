scriptcs-edge
=============

# What is it?

Execute node.js scripts from scriptcs with the power of [edge] (https://github.com/tjanczuk/edge). Script on script!

Edge allows invoking node.js applications from within .NET! scriptcs-edge makes it easy to consume edge within your scriptcs scripts.

# Platform requirements
Currently only Windows is supported as the edge nuget package only works on Windows.

# Install
```
scriptcs -install ScriptCs.Edge
```

# Sample Usage
```csharp
Require<EdgePack>();

//Edge.Func is static so no need to use the return value.
Edge.Func(
	@"return function(data, callback) {
		callback(null, 'Node js ' + process.version + ' welcomes ' + data);
	}"
);

var msg = (string) func(".NET").Result;
Console.WriteLine(msg);
```

# Notes
This is a work in progress. The Edge library is very new and we have not tested it heavily with scriptcs. There are no known issues at this time, but this may change. 

# Acknowledgments
Thanks to [Tomasz Janczuk] (https://github.com/tjanczuk) for the awesomeness of edge, and for his advice in the design of this script pack, as well as unblocking issues that were affecting this implementation.

