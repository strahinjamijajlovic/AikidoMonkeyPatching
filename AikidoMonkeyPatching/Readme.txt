Hello to anyone reading this! 

Just want to give a bit of an insight into what I did and what I learned along the way.

I went into a bit of a research about monkey patching, how it works and why it's generally considered a bad practice in .NET. 
I found that while it maybe can be useful in some scenarios, you often end up with code that's hard to maintain and debug.
On top of that, it can introduce unexpected behavior, especially if multiple patches are applied to the same method or class. Sounds like a nightmare scenario honestly. :)

So, I tried achieving it just as an example, and also proposed some better alternatives.

What I ended up with is 4 examples:

1. Attempt at true monkey patching using reflection and replacing the method at runtime.

	This failed, since I got blocked by the CLR, I even tried .NET Core 3.1 as the oldest version I had, but it still didn't work. Probably would work with 4.7.2
	But that is great! I don't want my CLR to allow this kind of shanenegens. :) 
	So I gave up on it, and moved to Harmony.

2. Using Harmony, a library that allows you to patch methods "safely"

	I was able to patch methods at runtime nicely, and it worked as expected.
	You can intercept the method before it runs, after it runs. You can choose to not invoke the original method etc.
	It made me wonder how this is even possible, but I guess it uses some IL rewriting magic under the hood.

3. Diagnostic Listener
	
	Along the way I found out you can use DiagnosticListener and DiagnosticSource to intercept method calls.
	Not completely relevant since you can't change the method behavior.
	But it's kinda more "official" way of doing it in .NET. It allows you to add your own listeners to methods without modifying the original code.
	It also provides a way to log and handle groups of events, not just single method call. 
	This could be useful for logging, telemetry, or other cross-cutting concerns.
	
4. And finally, using a simple decorator to achieve similar results without monkey patching

	Sure it's not monkey patching and you have another wrapper class (which is a good thing I would say)
	But it allows you to achieve similar results without the risks associated with monkey patching.
	And it's very clean, easy to understand, easy to maintain, all the good stuff!

So that's my little journey into the world of monkey patching in .NET. :)
Curious to hear your thoughts and why you were interested in it? 
Is it somehow useful in cyber-security or relevant to Zen?