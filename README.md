Spring.FluentContext
===========

Allows creating Spring.NET IoC container from code using fluent way:
* without XML
* without literals (while it is also possible)
* without any static classes or objects (it is possible to create as many context as are needed)
* with type-safe injections
* with possibility to integrate with other contexts (like XML ones) using hierarchical context construction

Implemented API Features:
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
* Setter injection:
	* with constants
	* with registered objects
	* with inline object definition
* Lookup Method injection with registered objects
* Autowiring
* Aliasing
* Indirect dependencies creation (allowing to specify order of objects creation / destruction)
* Init / Destroy method call specification 
* Dependency check
* Type-safe, literal-less references


Not Implemented Features:
* lazy and non-lazy initialization (now everything is initialized lazily)
* classes for easy collection handling like initializing object dependency using a list of other registered objects

Limitations:
* it is not possible to use lambda expressions to point protected methods or properties with non-public getters, that is why API allows also to use literals.

Examples and help:
* Solution contains Spring.FluentContext.Examples presenting usage of implemented features
* All implemented features contains also unit tests which shows also its usage
* Please also see the wiki page for details how to use this project