package com.eselmser.MphasisTest;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.firefox.FirefoxDriver;

import cucumber.api.PendingException;
import cucumber.api.java.en.Given;
import cucumber.api.java.en.Then;
import cucumber.api.java.en.When;

public class MphasisTestStepImpl {
	
	private WebDriver driver;
	private String loginUrl = "";
	
	@Given("^At login page$")
	public void at_login_page() throws Throwable {
	    driver = new FirefoxDriver();
	    driver.get(loginUrl);
	}

	@Given("^User is registered with a role A$")
	public void user_is_registered_with_a_role_A() throws Throwable {
	    //Verify database has a test user with role A
	}

	@When("^User login$")
	public void user_login() throws Throwable {
		String usernameXpath = "";
		String passwordXpath = "";
		String loginButtonXpath = "";
		String username = "";//these could be parameterized in scenario outline example table
		String password = "";
	    driver.findElement(By.xpath(usernameXpath)).sendKeys(username);
	    driver.findElement(By.xpath(passwordXpath)).sendKeys(password);
	    driver.findElement(By.xpath(loginButtonXpath)).submit();
	}

	@Then("^Validate role A$")
	public void validate_role_A() throws Throwable {
	   //Check somewhere on page for the role
	}

	@Then("^Page redirect based on role A$")
	public void page_redirect_based_on_role_A() throws Throwable {
	    //Check page title to make sure the page that the dispatcher sent back to us after login in the one
		//that role A should be at
	}
	
	//Pretty much repeat for B.  Could parameterize role too to increase reusability

	@Given("^User is registered with a role B$")
	public void user_is_registered_with_a_role_B() throws Throwable {
	   //
	}

	@Then("^Validate role B$")
	public void validate_role_B() throws Throwable {
	   //
	}

	@Then("^Page redirect based on role B$")
	public void page_redirect_based_on_role_B() throws Throwable {
	    //
	}

}
