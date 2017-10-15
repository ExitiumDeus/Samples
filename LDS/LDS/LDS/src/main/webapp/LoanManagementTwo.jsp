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
						<li class="active"><form class="navbar-form pull-right"
								action="Home.do">
								<button type="submit" class="btn" value="Home">Home</button>
							</form></li>
						<li class="active"><form class="navbar-form pull-right"
								action="AboutMe.do">
								<button type="submit" class="btn" value="About">About</button>
							</form></li>

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
		<!-- toolbar -->
		<form class="navbar-form pull-right" action="Loans.do">
			<div class="navbar navbar-class">
				<div class="navbar-inner">
					<div class="container">
						<button type="button" class="btn btn-navbar"
							data-toggle="collapse" data-target=".nav-collapse">
							<span class="icon-bar"></span> <span class="icon-bar"></span> <span
								class="icon-bar"></span>
						</button>
						<a class="brand" href="#">Loans</a>
						<div class="nav-collapse collapse">
								<input name="action" type="submit"
									class="btn" value="new"></input> <input name="action" type="submit"
									class="btn" value="edit"></input> <input name="action" type="submit"
									class="btn" value="delete"></input> 
<!-- 									<button name = "action" type="submit" class="btn" -->
<!-- 									value="ok">OK</button> -->

							<!-- table -->
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
										<td><input name="loan" type="radio" class=""
											value="${temp.getLoanId()}"></input>  </td>
									</tr>
								</c:forEach>
							</table>


						</div>
						<!--/.nav-collapse -->
					</div>
				</div>

			</div>
		</form>


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
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">

</body>
</html>
