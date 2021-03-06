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
			<h1>Loan Management</h1>
		</div>

		<!-- Example row of columns -->
		<div class="row">
			<div class="span4">
				<h2>Create Loan</h2>
				<form class="form-horizontal" action="CreateLoan.do">
				<input name = "loanId" class="span2" placeholder="Loan ID(If Editing a Loan)" type="text"> 
				<input name = "lender" class="span2" placeholder="Lender ID" type="text"> 
				<input name = "propertyCity" class="span2" placeholder="Loan City" type="text"> 
				<input name = "propertyAddress" class="span2" placeholder="Loan Address" type="text">
				<input name = "propertyState" class="span2" placeholder="Loan State" type="text">  
				<input name = "propertyZip" class="span2" placeholder="Loan Zipcode" type="text"> 
				<input name = "loanTerm" class="span2" placeholder="Loan Term" type="text">				
				<input name = "borrowerName" class="span2" placeholder="Borrowers Name" type="text">
				<input name = "amount" class="span2" placeholder="Amount" type="text">
				<input name = "rate" class="span2" placeholder="Rate Name" type="text">
				<input name = "ARM" class="span2" type="checkbox">ARM</input>  
			
<!-- 				<input name = "archivedE" class="span2"  type="checkbox">Archived</input>  -->
				</br>
				<input name = "process" type="radio" class="" value="Create Loan">Create Loan</input>
				<input name = "process" type="radio" class="" value="Edit Loan">Edit Loan</input>

				<input type="submit" class="btn" value="Submit Loan"></input>

				</form>
			</div>
			
<!-- 			<div class="span4"> -->
<!-- 				<h2>Edit Loan</h2> -->
<!-- 				<form class="form-horizontal" action="EditLoan.do"> -->
<!--  				<input class="span2" placeholder="Loan ID" type="text">  --> 
<!-- 				<input name = "lenderE" class="span2" placeholder="Lender ID" type="text">  -->
<!-- 				<input name = "loanCityE" class="span2" placeholder="Loan City" type="text">  -->
<!-- 				<input name = "loanAddressE" class="span2" placeholder="Loan Address" type="text">  -->
<!-- 				<input name = "loanZipE" class="span2" placeholder="Loan Zipcode" type="text">  -->
<!-- 				<input name = "loanTermE" class="span2" placeholder="Loan Term" type="text">				 -->
<!-- 				<input name = "borrowerNameE" class="span2" placeholder="Borrowers Name" type="text"> -->
<!-- 				<input name = "ARME" class="span2" type="checkbox">ARM</input>   -->
<!-- 				<input name = "archivedE" class="span2"  type="checkbox">Archived</input>  --> 
<!-- 				<input type="submit" class="btn" value="Edit Loan"></input> -->

<!-- 				</form> -->
<!-- 			</div> -->
			<div class="span4">
				<h2>View Loan</h2>
				<form class="form-horizontal" action="ViewLoan.do">

				<input name ="loanIdV" class="span2" placeholder="Loan ID" type="text"> <input
					type="submit" class="btn" value="View Loan"></input>

				</form>

			</div>

			<div class="span4">
				<h2>View Archived Loan</h2>
				<form class="form-horizontal" action="ViewArchivedLoan.do">

				<input name = "loanIdA" type="submit" class="btn" value="View Archived Loan"></input>

				</form>
		
		<hr>
		</div>
		
		<div class="span4">
				<h2>Delete Loan</h2>
				<form class="form-horizontal" action="DeleteLoan.do">

				<input name ="loanIdD" class="span2" placeholder="Loan ID" type="text"> <input
					type="submit" class="btn" value="Delete Loan"></input>

				</form>

			</div>
		</div>
		</br>

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
	<link rel="stylesheet" type="text/css"
		href="css/bootstrap.css">
	<link rel="stylesheet" type="text/css"
		href="css/bootstrap.min.css">

</body>
</html>
