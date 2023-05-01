## MVVM using Caliurn Micro:
Creating a single window application and binding controls to properties and methods using naming convention was staright forward.

Challenges:
1. When introducing a second view, especially a usercontrol to be used in the first view, I was not able to hook up the view model to the view or vice versa :
	Caliburn Micro prefers ViewModel first approach. 
	That is ViewModel is first constructed then the caliburn micro view locator locates the view and constructs it.
	This is what we do in the bootstrapper class, we inform the framework using DisplayRootViewFor in OnStartup method to boot up with the corresponding view model, the locater identifes and initializes the view.
	In case of usercontrol view we need a way to initialize the viewmodel and view. In case of ViewModel first approach, we need to create a prop of that usercontrol's ViewModel in the mainviewmodel (where we need to use the usercontrol) and add a ContentControl in the view with x:Name key same as the prop, this will initialize the view and hook up the viewmodel and view together
	
	Problem in this approach:
	In design time I am unable to view the usercontrol in the designer
	Not able to set any properties of the usercontrol as the usercontrol is being loaded by the contentcontrol
	And dont know how Binding to the mainview properties would work
	
	References:
	https://blog.xoc.net/2018/06/using-usercontrols-with-caliburnmicro.html - viewmodel first approach
	
	Tried the view first approach from the same article referenced above, were able to solve the design time no preview problem.
	It involves:
	0. in xaml of the main view, include the usercontrol's view and bind to the vm using cm:Bind.Model where xmlns:cm="http://www.caliburnproject.org"
	1. Creating a resource in the usercontrol of the viewmodel. 
	2. Setting the root element's DataContext to this resource.
	3. And in the code behind (xaml.cs) add a prop of the viewmodel type and initialize it to the DataContext of the root (vm = (FolderWithImagePreviewViewModel)this.rootGrid.DataContext;)
	4. Add a wrapper property for the prop in viewmodel
	
	Problems with this approach:
	1. First of all there are lot of code behind
	2. The wrapper property is not bindable
	3. I made the wrapper property bindable by converting it to dependency property. Then there is no exception, and the binding didnt happen as well.
	
	
2. I was able to introduce a window as a second view and it worked well.
	We got to create a property for IWindowManager of Caliburn Micro, using which we can launch the window async. I am yet to figure out how to use it to launch a model window.
