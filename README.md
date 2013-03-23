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
* The project documentation is available on [Wiki](https://github.com/Suremaker/Spring.FluentContext/wiki) page.
* Solution contains also [Spring.FluentContext.Examples](https://github.com/Suremaker/Spring.FluentContext/tree/master/Spring.FluentContext.Examples) project presenting usage of major features.
* All implemented features contains unit tests which shows also how to use them.

## Implemented API Features
* [Registration of objects](https://github.com/Suremaker/Spring.FluentContext/wiki/Object-definition-registration):
	* with specified id
	* with default id
	* with unique id (automatically generated)
	* as singleton / prototype
* [Registration of existing singleton instances with specified, default or unique id](https://github.com/Suremaker/Spring.FluentContext/wiki/Object-definition-registration)
* [Registration of AOP Proxy Factories for given interface](https://github.com/Suremaker/Spring.FluentContext/wiki/AOP-Proxy-Factories):
	* support for multiple interceptors addition
	* support for type-safe targeting
	* support for defining if factory supposed to create proxies as singleton or prototype
* [Object instantiation using](https://github.com/Suremaker/Spring.FluentContext/wiki/Object-instantiation):
	* selected or matching constructor
	* static factory method
	* factory object
* [Constructor injection](https://github.com/Suremaker/Spring.FluentContext/wiki/Constructor-Injection):
	* with constants
	* with registered objects
	* with inline object definition
	* with collections (if it is applicable to constructor parameter type)
* [Setter injection](https://github.com/Suremaker/Spring.FluentContext/wiki/Setter-Injection):
	* with constants
	* with registered objects
	* with inline object definition
	* with collections (if it is applicable to property type)
* [Lookup Method injection with registered objects](https://github.com/Suremaker/Spring.FluentContext/wiki/Lookup-Method-Injection)
* [Collection injection (array/list/dictionary) with possibility to mix values, object references and inline object definitions](https://github.com/Suremaker/Spring.FluentContext/wiki/Collection-injection)
* [Autowiring](https://github.com/Suremaker/Spring.FluentContext/wiki/Autowiring)
* [Aliasing](https://github.com/Suremaker/Spring.FluentContext/wiki/Aliasing)
* [Indirect dependencies creation](https://github.com/Suremaker/Spring.FluentContext/wiki/Indirect-dependencies) (allowing to specify order of objects creation / destruction)
* [Init / Destroy method call specification ](https://github.com/Suremaker/Spring.FluentContext/wiki/Initialization-and-finalization)
* [Dependency check](https://github.com/Suremaker/Spring.FluentContext/wiki/Dependency-checking)
* Type-safe, literal-less references

## Not Implemented Features
* lazy and non-lazy initialization (now everything is initialized lazily)

## Limitations
* it is not possible to use lambda expressions to point protected methods or properties with non-public getters, that is why API allows also to use literals.
