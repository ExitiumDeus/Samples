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


<!-- HTML5 shim, for IE6-8 support of HTML5 elements -->
<!--[if lt IE 9]>
      <script src="src/main/webapp/js/html5shiv.js"></script>
    <![endif]-->

<!-- Fav and touch icons -->
<link rel="apple-touch-icon-precomposed" sizes="144x144"
	href="../assets/ico/apple-touch-icon-144-precomposed.png">
<link rel="apple-touch-icon-precomposed" sizes="114x114"
	href="../assets/ico/apple-touch-icon-114-precomposed.png">
<link rel="apple-touch-icon-precomposed" sizes="72x72"
	href="../assets/ico/apple-touch-icon-72-precomposed.png">
<link rel="apple-touch-icon-precomposed"
	href="../assets/ico/apple-touch-icon-57-precomposed.png">
<link rel="shortcut icon" href="../assets/ico/favicon.png">
</head>

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
						<li class="active"><a href="#">Home</a></li>
						<li><a href="#about">About</a></li>
						<li><a href="#contact">Contact</a></li>
						<li class="dropdown"><a href="#" class="dropdown-toggle"
							data-toggle="dropdown">Dropdown <b class="caret"></b></a>
							<ul class="dropdown-menu">
								<li><a href="#">Action</a></li>
								<li><a href="#">Another action</a></li>
								<li><a href="#">Something else here</a></li>
								<li class="divider"></li>
								<li class="nav-header">Nav header</li>
								<li><a href="#">Separated link</a></li>
								<li><a href="#">One more separated link</a></li>
							</ul></li>
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
			<h1>Lender Management</h1>
		</div>

		<!-- Example row of columns -->
		<div class="row">
			<div class="span4">
				<h2>Create Lender</h2>
				<p></p>
				
				<form class="form-horizontal" uri="CreateLender.do">
						<div class="controls">
							<input class="span2" placeholder="Lender ID" type="text">
							<input class="span2" placeholder="Lender Name" type="text">							
							<input type="submit" class="btn" >View Lender</input>
						</div>
					</div>
				</form>				
			</div>
			<div class="span4">
				<h2>View Lender</h2>
				<p></p>
					<form class="form-horizontal" uri="ViewLender.do">					
					<div class="control-group">
						<div class="controls">
							<input class="span2" placeholder="Lender ID" type="text">
							<input type="submit" class="btn" >View Lender</input>
						</div>
					</div>
				</form>
				
			</div>
			<div class="span4">
				<h2>Edit Lender</h2>
					<form class="form-horizontal" uri="EditLender.do">					
					<div class="control-group">
						<div class="controls">
							<input class="span2" placeholder="Lender ID" type="text">
							<input class="span2" placeholder="Lender Name" type="text">	
							<input type="submit" class="btn" >Edit Lender</input>
						</div>
					</div>
				</form>
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
		href="css/boostrap-reponsive.min.css">
	<link rel="stylesheet" type="text/css" href="css/bootstrap.css">
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">

</body>
</html>
