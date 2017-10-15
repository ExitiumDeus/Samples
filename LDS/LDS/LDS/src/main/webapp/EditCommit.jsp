<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="utf-8">
<title>Loan Delivery System</title>
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<meta name="description" content="">
<meta name="author" content="">
<%@taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core"%>

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
				<h2>Edit Commit</h2>
				<h5>Commit Id: ${commit}</h5>
				<form class="form-horizontal" action="EditCommit.do">
				<!-- table of available and one for loans in -->
				<div><h3>Available Loans</h3>
				<table>
								<tr>
									<th>Loan ID  </th>
									<th>Lender ID  </th>
									<th>Loan Term  </th>
									<th>Property Address  </th>
									<th>Property City  </th>
									<th>Property State  </th>
									<th>Property Zip  </th>
									<th>Borrower's Name  </th>
									<th>ARM  </th>
									<th>Amount  </th>
									<th>Rate  </th>
									<th>Archived  </th>
									<th>Select  </th>
								</tr>
								<c:forEach var="temp" items="${data}">
									<tr>
										<td>${temp.getLoanId()}  </td>
										<td>${temp.getLenderId().getLenderId()}  </td>
										<td>${temp.getLoanTerm()}  </td>
										<td>${temp.getPropertyAddress()}  </td>
										<td>${temp.getPropertyCity()}  </td>
										<td>${temp.getPropertyState()}  </td>
										<td>${temp.getPropertyZip()}  </td>
										<td>${temp.getBorrowerName()}  </td>
										<td>${temp.isARM()}  </td>
										<td>${temp.getAmount()}  </td>
										<td>${temp.getRate()}  </td>
										<td>${temp.isArchived()}  </td>
										<td><input name="loanA" type="radio" class=""
											value="${temp.getLoanId()}"></input>  </td>
									</tr>
								</c:forEach>
							</table>
							<input name = "action" type="submit" class="btn" value="Add Selected"></input>
				
				</div>
				<br>
				<div><h3>Current Loans</h3>
				<table>
								<tr>
									<th>Loan ID  </th>
									<th>Lender ID  </th>
									<th>Loan Term  </th>
									<th>Property Address  </th>
									<th>Property City  </th>
									<th>Property State  </th>
									<th>Property Zip  </th>
									<th>Borrower's Name  </th>
									<th>ARM  </th>
									<th>Amount  </th>
									<th>Rate  </th>
									<th>Archived  </th>
									<th>Select  </th>
								</tr>
								<c:forEach var="temp1" items="${data1}">
									<tr>
										<td>${temp1.getLoanId()}  </td>
										<td>${temp1.getLenderId().getLenderId()}  </td>
										<td>${temp1.getLoanTerm()}  </td>
										<td>${temp1.getPropertyAddress()}  </td>
										<td>${temp1.getPropertyCity()}  </td>
										<td>${temp1.getPropertyState()}  </td>
										<td>${temp1.getPropertyZip()}  </td>
										<td>${temp1.getBorrowerName()}  </td>
										<td>${temp1.isARM()}  </td>
										<td>${temp1.getAmount()}  </td>
										<td>${temp1.getRate()}  </td>
										<td>${temp1.isArchived()}  </td>
										<td><input name="loanC" type="radio" class=""
											value="${temp1.getLoanId()}"></input>  </td>
									</tr>
								</c:forEach>
							</table>
							<input name = "action" type="submit" class="btn" value="Remove Selected"></input>
				</div>

				<input name = "action" type="submit" class="btn" value="Return to Commit Management"></input>
				<input name = "commitId" type="hidden" class="btn" value="${commit}"></input>

				</form>
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
		href="css/bootstrap.min.css">

</body>
</html>
