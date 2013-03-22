Spring.FluentContext
===========

Allows creating Spring.NET IoC container from code using fluent API:
* without XML
* without literals (while it is also possible to use them)
* without any static classes or objects (it is possible to create as many contexts as are needed)
* with type-safe injections
* with possibility to integrate with other contexts (like XML ones) using hierarchical context construction

## Download
It is possible to download package using [NuGet](http://nuget.org): `PM> Install-Package Spring.FluentContext`

or compile from sources available on Git: `git clone git://github.com/Suremaker/Spring.FluentContext.git`

## Examples and help
* The project documentation is available on [Wiki](Spring.FluentContext/wiki) page.
* Solution contains also [Spring.FluentContext.Examples](Spring.FluentContext/tree/master/Spring.FluentContext.Examples) project presenting usage of major features.
* All implemented features contains unit tests which shows also how to use them.

## Implemented API Features
* Registration of objects:
	* with specified id
	* with default id
	* with unique id (automatically generated)
	* as singleton / prototype
* Registration of AOP Proxy Factories for given interface:
	* support for multiple interceptors addition
	* support for type-safe targeting
	* support for defining if factory supposed to create proxies as singleton or prototype
* Registration of existing singleton instances with specified, default or unique id
* Object instantiation using:
	* selected or matching constructor
	* static factory method
	* factory object
* Constructor injection:
	* with constants
	* with registered objects
	* with inline object definition
	* with collections (if it is applicable to constructor parameter type)
* Setter injection:
	* with constants
	* with registered objects
	* with inline object definition
	* with collections (if it is applicable to property type)
* support for collections (array/list/dictionary) with possibility to mix values, object references and inline object definitions
* Lookup Method injection with registered objects
* Autowiring
* Aliasing
* Indirect dependencies creation (allowing to specify order of objects creation / destruction)
* Init / Destroy method call specification 
* Dependency check
* Type-safe, literal-less references

## Not Implemented Features
* lazy and non-lazy initialization (now everything is initialized lazily)

## Limitations
* it is not possible to use lambda expressions to point protected methods or properties with non-public getters, that is why API allows also to use literals.
