import static org.junit.Assert.fail;

import java.math.BigDecimal;

import org.testng.annotations.Test;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;
import org.testng.Assert;
import org.testng.annotations.AfterClass;
import org.testng.annotations.AfterMethod;
import org.testng.annotations.AfterSuite;
import org.testng.annotations.AfterTest;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.BeforeSuite;
import org.testng.annotations.BeforeTest;

import com.jselmser.lds.BusinessObject;

public class RegressionTests {
	private BusinessObject bo;
	private ApplicationContext appContext;
	//Test Crud
	//Loan
	@Test( priority = 2)
	public void CreateLoan() {
		bo.CreateLoan(bo.FindTestLender(), 32, "Test Address", "Test City", "Test State", "Test Zip", "testBorrower", true, 10.0, new BigDecimal(1000000), false);
		Assert.assertEquals(bo.FindTestLoan().getBorrowerName(), "testBorrower", "");
	}
	@Test( priority = 3)
	public void RetrieveLoan() {
		Assert.assertEquals(bo.FindTestLoan().getBorrowerName(), "testBorrower", "");
		
	}
	@Test( priority = 4)
	public void UpdateLoan() {
		bo.EditLoan(bo.FindTestLoan().getLoanId(),bo.FindTestLender(), 32, "Test Address has been edited", "Test City", "Test State", "Test Zip", "testBorrower", true, 10.0, new BigDecimal(1000000), false);
		Assert.assertEquals(bo.FindTestLoan().getPropertyAddress(), "Test Address has been edited", "");
	}
	@Test (priority = 6)
	public void DeleteLoan() {
		bo.DeleteLoan(bo.FindTestLoan().getLoanId());
		Assert.assertTrue(bo.FindTestLoan().isArchived());
		
	}
	
	//Lender
	@Test( priority = 1)
	public void CreateLender() {
		bo.CreateLender("testLender");
		Assert.assertEquals(bo.FindTestLender().getLenderName(), "testLender", "");
	}
	@Test( priority = 2)
	public void RetrieveLender() {
		Assert.assertEquals(bo.FindTestLender().getLenderName(), "testLender", "");
	}
	@Test( priority = 3)
	public void UpdateLender() {
		bo.EditLender(bo.FindTestLender().getLenderId(), "testLender has been edited");
		Assert.assertEquals(bo.FindTestLender().getLenderName(), "testLender has been edited", "");
	}
	@Test (priority = 6)
	public void DeleteLender() {
		Assert.assertFalse(false); //not implemented
	}
	
	//Commit
	@Test( priority = 3)
	public void CreateCommit() {
		bo.CreateCommit("testCommit");
		Assert.assertEquals(bo.FindTestCommit().getCommitName(), "testCommit", "");
	}
	@Test( priority = 4)
	public void RetrieveCommit() {
		Assert.assertEquals(bo.FindTestCommit().getCommitName(), "testCommit", "");
	}
	@Test( priority = 5)
	public void UpdateCommit() {
		bo.AddLoanToCommmit(bo.FindTestLoan().getLoanId(), bo.FindTestCommit().getCommitId());
		Assert.assertEquals(bo.FindTestCommit().getListOfLoans().get(0), bo.FindTestLoan(), "");
		bo.RemoveLoanFromCommit(bo.FindTestLoan().getLoanId(), bo.FindTestCommit().getCommitId());
		Assert.assertTrue(bo.FindTestCommit().getListOfLoans().isEmpty());
	}
	@Test (priority = 6)
	public void DeleteCommit() {
		bo.DeleteCommit(bo.FindTestCommit().getCommitId());
		Assert.assertTrue(bo.FindTestCommit().isArchived());
	}

	@BeforeMethod
	public void beforeMethod() {
		System.out.println("before");
	}

	@AfterMethod
	public void afterMethod() {
		System.out.println("after");
	}

	// @DataProvider
	// public Object[][] dp() {
	//
	// }
	@BeforeClass
	public void beforeClass() {
		System.out.println("before class");
	}

	@AfterClass
	public void afterClass() {
		System.out.println("after class");
	}

	@BeforeTest
	public void beforeTest() {
		System.out.println("before test");

	}

	@AfterTest
	public void afterTest() {
		System.out.println("after test");
	}

	@BeforeSuite
	public void beforeSuite() {
		System.out.println("before suite");
		appContext = new ClassPathXmlApplicationContext("/WEB-INF/ApplicationContext.xml");
		bo = (BusinessObject) appContext.getBean("BusinessObject");
	}

	@AfterSuite
	public void afterSuite() {
		System.out.println("after suite");
	}

}
