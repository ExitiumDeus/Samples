<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="utf-8">
<title>Loan Delivery System</title>
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<meta name="description" content="">
<meta name="author" content="">

<!-- Le styles -->

<style type="text/css">
body {
	padding-top: 60px;
	padding-bottom: 40px;
}
</style>



<body>

	<div class="navbar navbar-inverse navbar-fixed-top">
		<div class="navbar-inner">
			<div class="container">
				<button type="button" class="btn btn-navbar" data-toggle="collapse"
					data-target=".nav-collapse">
					<span class="icon-bar"></span> <span class="icon-bar"></span> <span
						class="icon-bar"></span>
				</button>
				<a class="brand" href="#">Loan Delivery Service</a>
				<div class="nav-collapse collapse">
					<ul class="nav">
				<li class="active" ><form class="navbar-form pull-right" action="Home.do"><button type="submit" class="btn" value = "Home">Home</button></form></li>
						<li class="active" ><form class="navbar-form pull-right" action="AboutMe.do"><button type="submit" class="btn" value = "About">About</button></form></li>
						
						
					</ul>
					<form class="navbar-form pull-right">
						<input class="span2" type="text" placeholder="Email"> <input
							class="span2" type="password" placeholder="Password">
						<button type="submit" class="btn">Sign in</button>
					</form>
				</div>
				<!--/.nav-collapse -->
			</div>
		</div>
	</div>

	<div class="container">

		<!-- Main hero unit for a primary marketing message or call to action -->
		<div class="hero-unit">
			<h1>LDS Management</h1>
		</div>

		<!-- Example row of columns -->
		<div class="row">
			<div class="span4">
				<h2>
					Loan </br> Management
				</h2>
				<form class="form-horizontal" action="ViewLoanTwo.do">
					</br> <input type="submit" value="Loan Management" class="btn"></input>

				</form>
			</div>
			<div class="span4">
				<h2>
					Commit </br> Management
				</h2>
				</br>
				<form class="form-horizontal" action="ViewCommitTwo.do">

					<input type="submit" class="btn" value="Commit Management"></input>

				</form>
			</div>
			<div class="span4">
				<h2>
					Lender </br> Management
				</h2>
				</br>
				<form class="form-horizontal" action="ViewLenderTwo.do">

					<input type="submit" class="btn" value="Lender Management"></input>

				</form>
			</div>
		</div>

		<hr>

		<footer>
			<p>&copy; Josh Selmser : eIntern</p>
		</footer>

	</div>
	<!-- /container -->

	<!-- Le javascript
    ================================================== -->
	<!-- Placed at the end of the document so the pages load faster -->
	<script src="src/main/webapp/js/jquery.js"></script>
	<script src="/js/bootstrap.min.js"></script>
	<link rel="stylesheet" type="text/css"
		href="css/bootstrap-reponsive.min.css">
	<link rel="stylesheet" type="text/css" href="css/bootstrap.css">
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">

</body>
</html>
