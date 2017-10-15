package com.jselmser.lds.servlet;

import java.math.BigDecimal;
import java.util.List;

import javax.servlet.http.HttpServletRequest;

import org.apache.log4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import com.jselmser.lds.BusinessObject;
import com.jselmser.lds.Commit;
import com.jselmser.lds.Lender;
import com.jselmser.lds.Loan;

@Controller
public class WebHandler {
	// local run test area without deploying.
	// ApplicationContext appContext = new
	// ClassPathXmlApplicationContext("spring/config/BeanLocations.xml");
	// create and run the dao?
	public static Logger logger = Logger.getRootLogger();

	@Autowired
	BusinessObject bo;
	// public WebHandler(){
	// bo = new BusinessObject();
	// }

	// Redirects
	@RequestMapping(value = "Home.do", method = RequestMethod.GET)
	private String HandleHome(ModelMap map, HttpServletRequest req) {
		logger.fatal(bo.toString());
		;
		return "index";
	}

	@RequestMapping(value = "LoanManagement.do", method = RequestMethod.GET)
	private String HandleLoanManagement(ModelMap map, HttpServletRequest req) {

		return "LoanManagement";
	}

	@RequestMapping(value = "LenderManagement.do", method = RequestMethod.GET)
	private String HandleLenderManagement(ModelMap map, HttpServletRequest req) {

		return "LenderManagement";
	}

	@RequestMapping(value = "CommitManagement.do", method = RequestMethod.GET)
	private String HandleCommitManagement(ModelMap map, HttpServletRequest req) {

		return "CommitManagement";
	}

	@RequestMapping(value = "AboutMe.do", method = RequestMethod.GET)
	private String HandleAboutMe(ModelMap map, HttpServletRequest req) {

		return "AboutMe";
	}

	@RequestMapping(value = "ArchivedLoans.do", method = RequestMethod.GET)
	private String HandleArchivedLoans(ModelMap map, HttpServletRequest req) {

		return "ArchivedLoans";
	}

	@RequestMapping(value = "ArchivedCommits.do", method = RequestMethod.GET)
	private String HandleArchivedCommits(ModelMap map, HttpServletRequest req) {

		return "ArchivedCommits";
	}

	// BO Specific logic
	@RequestMapping(value = "SubmitCommit.do", method = RequestMethod.GET)
	private String HandleSubmitCommit(ModelMap map, HttpServletRequest req) {
		int commitId = Integer.parseInt(req.getParameter("submitCommit"));
		bo.SubmitCommit(commitId);
		return "CommitManagement";
	}

	//// Create
	@RequestMapping(value = "CreateLoan.do", method = RequestMethod.GET)
	private String HandleCreateLoan(ModelMap map, HttpServletRequest req) {
		// Inputs

		Lender lender = (Lender) bo.FindLenderById(Integer.parseInt((String) req.getParameter("lender")));
		int loanTerm = Integer.parseInt(req.getParameter("loanTerm"));
		String propertyAddress = req.getParameter("propertyAddress");
		String propertyCity = req.getParameter("propertyCity");
		String propertyState = req.getParameter("propertyState");
		String propertyZip = req.getParameter("propertyZip");
		String borrowerName = req.getParameter("borrowerName");
		double rate = Double.parseDouble(req.getParameter("rate"));
		BigDecimal amount = new BigDecimal(Double.parseDouble(req.getParameter("amount")));
		String archivedStr = req.getParameter("archived");
		String ARMStr = req.getParameter("ARM");
		boolean archived, ARM;
		if (archivedStr != null) {
			archived = true;
		} else
			archived = false;
		if (ARMStr != null) {
			ARM = true;
		} else
			ARM = false;

		// int loanId = Integer.parseInt(req.getParameter("loanId"));
		bo.CreateLoan(lender, loanTerm, propertyAddress, propertyCity, propertyState, propertyZip, borrowerName, ARM,
				rate, amount, archived);

		System.out.println(propertyAddress);
		System.out.println(propertyCity);
		System.out.println(propertyState);
		System.out.println(propertyZip);
		System.out.println(borrowerName);

		List<Loan> list = bo.ViewLoan();
		req.getSession().setAttribute("data", list);
		req.getSession().setAttribute("type", "Loan.class");

		return "LoanManagementTwo"; // maybe return view table?
	}

	@RequestMapping(value = "EditLoan.do", method = RequestMethod.GET)
	private String HandleEditLoan(ModelMap map, HttpServletRequest req) {
		// Inputs

		Lender lender = (Lender) bo.FindLenderById(Integer.parseInt((String) req.getParameter("lender")));
		int loanTerm = Integer.parseInt(req.getParameter("loanTerm"));
		String propertyAddress = req.getParameter("propertyAddress");
		String propertyCity = req.getParameter("propertyCity");
		String propertyState = req.getParameter("propertyState");
		String propertyZip = req.getParameter("propertyZip");
		String borrowerName = req.getParameter("borrowerName");
		double rate = Double.parseDouble(req.getParameter("rate"));
		BigDecimal amount = new BigDecimal(Double.parseDouble(req.getParameter("amount")));
		String archivedStr = req.getParameter("archived");
		String ARMStr = req.getParameter("ARM");
		boolean archived, ARM;
		if (archivedStr != null) {
			archived = true;
		} else
			archived = false;
		if (ARMStr != null) {
			ARM = true;
		} else
			ARM = false;

		int loanId = Integer.parseInt(req.getParameter("loanId"));
		bo.EditLoan(loanId, lender, loanTerm, propertyAddress, propertyCity, propertyState, propertyZip, borrowerName,
				ARM, rate, amount, archived);

		System.out.println(propertyAddress);
		System.out.println(propertyCity);
		System.out.println(propertyState);
		System.out.println(propertyZip);
		System.out.println(borrowerName);

		List<Loan> list = bo.ViewLoan();
		req.getSession().setAttribute("data", list);
		req.getSession().setAttribute("type", "Loan.class");

		return "LoanManagementTwo"; // maybe return view table?
	}

	@RequestMapping(value = "CreateLender.do", method = RequestMethod.GET)
	private String HandleCreateLender(ModelMap map, HttpServletRequest req) {
		// Inputs
		// int lenderId = Integer.parseInt(req.getParameter("lenderId"));

		String lenderName = req.getParameter("lenderName");
		// String process = req.getParameter("process");
		// if (process.equals("Create Lender")) {
		bo.CreateLender(lenderName);
		// } else if (process.equals("Edit Lender")) {
		// bo.EditLender(lenderId, lenderName);
		// } else {
		// return "Error";
		// }

		return "LenderManagementTwo"; // maybe return view table?
	}

	@RequestMapping(value = "EditCommit.do", method = RequestMethod.GET)
	private String HandleCreateCommit(ModelMap map, HttpServletRequest req) {
		// Inputs
		// code to add remove loans, view tables of available and loans that are
		// part of the commit
		

		String[] submitValues = req.getParameterValues("action");
		String submitValue = ""; //should be submit action
		 if(submitValues != null && submitValues.length == 1){
		 submitValue = submitValues[0];
		 } //else error
		 String[] commitAValues = req.getParameterValues("loanA");
		 String commitAValue = ""; //should be loan id
		 if(commitAValues != null && commitAValues.length == 1){
		 commitAValue = commitAValues[0];
		 } //else error
		 String[] commitCValues = req.getParameterValues("loanC");
		 String commitCValue = ""; //should be loan id
		 if(commitCValues != null && commitCValues.length == 1){
		 commitCValue = commitCValues[0];
		 } //else error
		
		 if(submitValue.equals("Add Selected")){
			 	String commitIdString = (req.getParameter("commitId"));
			 	int commitId = Integer.parseInt(commitIdString);
			 	req.getSession().setAttribute("commit", bo.FindCommitById(commitId).getCommitId());
				int loanId = Integer.parseInt(commitAValue);
				bo.AddLoanToCommmit(loanId, commitId);
				// finalize new info to forward
				List<Loan> list = bo.ViewLoan();
				
				req.getSession().setAttribute("data", list);
				req.getSession().setAttribute("type", "Loan.class");
			

				Commit c = bo.FindCommitById(Integer.parseInt(req.getParameter("commitId")));
				req.getSession().setAttribute("data1", c.getListOfLoans());
				return "EditCommit"; //debug
		 } else if(submitValue.equals("Remove Selected")){ 
			 	int commitId = Integer.parseInt(req.getParameter("commitId"));
			 	req.getSession().setAttribute("commit", bo.FindCommitById(commitId).getCommitId());
				int loanId = Integer.parseInt(commitCValue);
				bo.RemoveLoanFromCommit(loanId, commitId);
				// finalize new info to forward
				List<Loan> list = bo.ViewLoan();
				req.getSession().setAttribute("data", list);
				req.getSession().setAttribute("type", "Loan.class");

				Commit c = bo.FindCommitById(Integer.parseInt(req.getParameter("commitId")));
				req.getSession().setAttribute("data1", c.getListOfLoans());
		
				return "EditCommit";
		 }
		 else if(submitValue.equals("Return to Commit Management")){
			// finalize new info to forward
				List<Commit> list = bo.ViewCommit();
				req.getSession().setAttribute("data", list);
				req.getSession().setAttribute("type", "Commit.class");				
				
//				Commit c = bo.FindCommitById(Integer.parseInt(req.getParameter("commitId")));
//				req.getSession().setAttribute("data1", c.getListOfLoans());
		
				return "CommitManagementTwo";
		 }
		

		// finalize new info to forward
//			List<Loan> list = bo.ViewLoan();
//			req.getSession().setAttribute("data", list);
//			req.getSession().setAttribute("type", "Loan.class");
//			//next two lines bug out but everything should be handled before reaching this point
//
//			Commit c = bo.FindCommitById(Integer.parseInt(req.getParameter("commit")));
//			req.getSession().setAttribute("data1", c.getListOfLoans());

		return "EditCommit"; // maybe return view table?
		// reference
	}

	//// View Tables
	// if nothing entered, show all?
	// if something entered check input for sqlinjection and then show that item
	@RequestMapping(value = "Loans.do", method = RequestMethod.GET)
	private String HandleLoanManagementTwo(ModelMap map, HttpServletRequest req) {
		// List<Loan> list = bo.ViewLoan();
		// req.getSession().setAttribute("data", list);
		// req.getSession().setAttribute("type", "Loan.class");

		// multiple different returns based on the value of the submit
		String[] submitValues = req.getParameterValues("action");
		String submitValue = ""; // should be submit action
		if (submitValues != null && submitValues.length == 1) {
			submitValue = submitValues[0];
		} // else error
		String[] loanValues = req.getParameterValues("loan");
		String loanValue = ""; // should be loan id
		if (loanValues != null && loanValues.length == 1) {
			loanValue = loanValues[0];
		} // else error

		if (submitValue.equals("new")) {
			return "CreateLoan"; // debug
		} else if (submitValue.equals("edit")) {
			if (!loanValue.equals("")) {
				req.getSession().setAttribute("loan", bo.FindLoanById(Integer.parseInt(loanValue)));
				return "EditLoan";
			}
		} else if (submitValue.equals("delete")) {

			int i = Integer.parseInt(req.getParameter("loan"));
			bo.DeleteLoan(i);
			List<Loan> list = bo.ViewLoan();
			req.getSession().setAttribute("data", list);
			req.getSession().setAttribute("type", "Loan.class");
			return "LoanManagementTwo";
		} 
		return "LoanManagementTwo";
	}

	@RequestMapping(value = "Lenders.do", method = RequestMethod.GET)
	private String HandleLenderManagementTwo(ModelMap map, HttpServletRequest req) {
		// List<Loan> list = bo.ViewLoan();
		// req.getSession().setAttribute("data", list);
		// req.getSession().setAttribute("type", "Loan.class");

		// multiple different returns based on the value of the submit
		String[] submitValues = req.getParameterValues("action");
		String submitValue = ""; // should be submit action
		if (submitValues != null && submitValues.length == 1) {
			submitValue = submitValues[0];
		} // else error
		String[] lenderValues = req.getParameterValues("lender");
		String lenderValue = ""; // should be loan id
		if (lenderValues != null && lenderValues.length == 1) {
			lenderValue = lenderValues[0];
		} // else error

		if (submitValue.equals("new")) {
			return "CreateLender"; // debug
		} else if (submitValue.equals("edit")) {
			if (!lenderValue.equals("")) {
				req.getSession().setAttribute("lender", bo.FindLoanById(Integer.parseInt(lenderValue)));
				return "EditLender";
			}
		} else if (submitValue.equals("delete")) {

			int i = Integer.parseInt(req.getParameter("lender"));
			// bo.DeleteLender(i);
			List<Lender> list = bo.ViewLender();
			req.getSession().setAttribute("data", list);
			req.getSession().setAttribute("type", "Lender.class");
			return "LenderManagement";
		}
		return "LoanManagementTwo";
	}

	@RequestMapping(value = "Commits.do", method = RequestMethod.GET)
	private String HandleCommitManagementTwo(ModelMap map, HttpServletRequest req) {
		// List<Loan> list = bo.ViewLoan();
		// req.getSession().setAttribute("data", list);
		// req.getSession().setAttribute("type", "Loan.class");

		// multiple different returns based on the value of the submit
		String[] submitValues = req.getParameterValues("action");
		String submitValue = ""; // should be submit action
		if (submitValues != null && submitValues.length == 1) {
			submitValue = submitValues[0];
		} // else error
		String[] commitValues = req.getParameterValues("commit");
		String commitValue = ""; // should be loan id
		if (commitValues != null && commitValues.length == 1) {
			commitValue = commitValues[0];
		} // else error

		if (submitValue.equals("new")) {
			bo.CreateCommit();
			List<Commit> list = bo.ViewCommit();
			req.getSession().setAttribute("data", list);
			req.getSession().setAttribute("type", "Commit.class");
			return "CommitManagementTwo"; // debug
		} else if (submitValue.equals("edit")) { // return page that lets you
													// add and remove loans to a
													// commit
			if (!commitValue.equals("")) {
				req.getSession().setAttribute("commit", bo.FindCommitById(Integer.parseInt(commitValue)).getCommitId());
				// finalize new info to forward
				List<Loan> list = bo.ViewLoan();
				req.getSession().setAttribute("data", list);
				req.getSession().setAttribute("type", "Loan.class");

				Commit c = bo.FindCommitById(Integer.parseInt(req.getParameter("commit")));
				req.getSession().setAttribute("data1", c.getListOfLoans());

				return "EditCommit";
			}
		} else if (submitValue.equals("delete")) {

			int i = Integer.parseInt(req.getParameter("commit"));
			bo.DeleteCommit(i);
			List<Commit> list = bo.ViewCommit();
			req.getSession().setAttribute("data", list);
			req.getSession().setAttribute("type", "Commit.class");
			return "CommitManagementTwo";
		} else if (submitValue.equals("submit")){
			int i = Integer.parseInt(req.getParameter("commit"));
			bo.SubmitCommit(i);
			List<Commit> list = bo.ViewCommit();
			req.getSession().setAttribute("data", list);
			req.getSession().setAttribute("type", "Commit.class");
			return "CommitManagementTwo";
			
		}
		return "CommitManagementTwo";
	}

	@RequestMapping(value = "ViewLoan.do", method = RequestMethod.GET)
	private String HandleViewLoan(ModelMap map, HttpServletRequest req) {
		List<Loan> list = bo.ViewLoan();
		req.getSession().setAttribute("data", list);
		req.getSession().setAttribute("type", "Loan.class");
		return "Table";
	}

	@RequestMapping(value = "ViewLoanTwo.do", method = RequestMethod.GET)
	private String HandleViewLoan2(ModelMap map, HttpServletRequest req) {
		List<Loan> list = bo.ViewLoan();
		req.getSession().setAttribute("data", list);
		req.getSession().setAttribute("type", "Loan.class");
		return "LoanManagementTwo";
	}

	@RequestMapping(value = "ViewLenderTwo.do", method = RequestMethod.GET)
	private String HandleViewLender2(ModelMap map, HttpServletRequest req) {
		List<Lender> list = bo.ViewLender();
		req.getSession().setAttribute("data", list);
		req.getSession().setAttribute("type", "Lender.class");
		return "LenderManagementTwo";
	}

	@RequestMapping(value = "ViewCommitTwo.do", method = RequestMethod.GET)
	private String HandleViewCommit2(ModelMap map, HttpServletRequest req) {
		List<Commit> list = bo.ViewCommit();
		req.getSession().setAttribute("data", list);
		req.getSession().setAttribute("type", "Commit.class");
		return "CommitManagementTwo";
	}

	@RequestMapping(value = "ViewArchivedLoan.do", method = RequestMethod.GET)
	private String HandleViewArchivedLoan(ModelMap map, HttpServletRequest req) {
		List<Loan> list = bo.ViewArchiveLoan();
		req.getSession().setAttribute("data", list);
		req.getSession().setAttribute("type", "Loan.class");
		return "Table";
	}

	@RequestMapping(value = "ViewLender.do", method = RequestMethod.GET)
	private String HandleViewLender(ModelMap map, HttpServletRequest req) {
		List<Lender> list = bo.ViewLender();
		req.getSession().setAttribute("data", list);
		req.getSession().setAttribute("type", "Lender.class");
		return "Table";
	}

	@RequestMapping(value = "ViewAllCommits.do", method = RequestMethod.GET)
	private String HandleViewAllCommit(ModelMap map, HttpServletRequest req) {
		List<Commit> list = bo.ViewCommit();
		req.getSession().setAttribute("data", list);
		req.getSession().setAttribute("type", "Commit.class");
		return "Table";
	}

	@RequestMapping(value = "ViewAllArchivedCommits.do", method = RequestMethod.GET)
	private String HandleViewArchivedCommit(ModelMap map, HttpServletRequest req) {
		List<Commit> list = bo.ViewArchiveCommit();
		req.getSession().setAttribute("data", list);
		req.getSession().setAttribute("type", "Commit.class");
		return "Table";
	}

	@RequestMapping(value = "ViewCommit.do", method = RequestMethod.GET)
	private String HandleViewCommit(ModelMap map, HttpServletRequest req) {
		int commitId = Integer.parseInt(req.getParameter("commitIdV"));
		List<Loan> list = bo.FindCommitById(commitId).getListOfLoans();
		req.getSession().setAttribute("data", list);
		req.getSession().setAttribute("type", "Loan.class");
		return "Table";
	}

	//// Edit
	@RequestMapping(value = "DeleteLoan.do", method = RequestMethod.GET)
	private String HandleDeleteLoan(ModelMap map, HttpServletRequest req) {
		int i = Integer.parseInt(req.getParameter("loanIdD"));
		bo.DeleteLoan(i);
		return "LoanManagement";
	}

	@RequestMapping(value = "DeleteCommit.do", method = RequestMethod.GET)
	private String HandleDeleteCommit(ModelMap map, HttpServletRequest req) {
		int i = Integer.parseInt(req.getParameter("commitIdD"));
		bo.DeleteCommit(i);
		return "CommitManagement";
	}

	// Add/remove to commit
	@RequestMapping(value = "AddLoan.do", method = RequestMethod.GET)
	private String HandleAddLoan(ModelMap map, HttpServletRequest req) {
		int commitId = Integer.parseInt(req.getParameter("commitIdA"));
		int loanId = Integer.parseInt(req.getParameter("loanIdA"));
		bo.AddLoanToCommmit(loanId, commitId);
		return "CommitManagement";
	}

	@RequestMapping(value = "RemoveLoan.do", method = RequestMethod.GET)
	private String HandleEditCommit(ModelMap map, HttpServletRequest req) {
		int commitId = Integer.parseInt(req.getParameter("commitIdR"));
		int loanId = Integer.parseInt(req.getParameter("loanIdR"));
		bo.RemoveLoanFromCommit(loanId, commitId);
		return "CommitManagement";
	}

}
