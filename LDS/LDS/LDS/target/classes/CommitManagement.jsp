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
			<h1>Commit Management</h1>
		</div>

		<!-- Example row of columns -->
		<div class="row">
			<div class="span4">
				<h2>Create Commit</h2>
				<form class="form-horizontal" action="CreateCommit.do">
					<input type="submit" class="btn" value="Create Commit"></input>
				</form>
			</div>
			<div class="span4">
				<h2>View Active Commits</h2>
				<form class="form-horizontal" action="ViewAllCommits.do">
					<input type="submit" class="btn" value="View Commit"></input>
				</form>
			</div>
			<div class="span4">
				<h2>View Archived Commits</h2>
				<form class="form-horizontal" action="ViewAllArchivedCommits.do">
					<input type="submit" class="btn" value="View Commit"></input>
				</form>
			</div>
			<div class="span4">
				<h2>View Commit</h2>
				<form class="form-horizontal" action="ViewCommit.do">
					<input name ="commitIdV"class="span2" placeholder="Commit ID" type="text"> <input
						type="submit" class="btn" value="View Commit"></input>
				</form>
			</div>
			<div class="span4">
				<h2>Delete Commit</h2>
				<form class="form-horizontal" action="DeleteCommit.do">

				<input name ="commitIdD" class="span2" placeholder="Loan ID" type="text"> <input
					type="submit" class="btn" value="Delete Commit"></input>

				</form>

			</div>
			<div class="span4">
				<h2>Remove Loan from Commit</h2>

				<form class="form-horizontal" action="RemoveLoan.do">

					<input name = "loanIdR" class="span2" placeholder="Loan ID" type="text"> <input name="commitIdR" 
						class="span2" placeholder="Commit ID" type="text"> <input
						type="submit" class="btn" value="Remove Loan"></input>

				</form>
			</div>
			<div class="span4">
				<h2>Add Loan to Commit</h2>

				<form class="form-horizontal" action="AddLoan.do">

				<input name = "loanIdA" class="span2" placeholder="Loan ID" type="text"> <input name="commitIdA" 
						class="span2" placeholder="Commit ID" type="text"> <input
						type="submit" class="btn" value="Add Loan"></input>

				</form>

			</div>
			<div class="span4"> -->
				<h2>Submit Commit</h2>
				<form class="form-horizontal" action="SubmitCommit.do">

				<input name ="submitCommit" class="span2" placeholder="Commit ID" type="text"> <input
					type="submit" class="btn" value="Submit Commit"></input>

				</form>

			</div>


			<hr>
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
		<link rel="stylesheet" type="text/css" href="css/bootstrap.css">
		<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
</body>
</html>
