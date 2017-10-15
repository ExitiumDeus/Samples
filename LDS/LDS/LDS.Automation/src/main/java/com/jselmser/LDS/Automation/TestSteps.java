package com.jselmser.LDS.Automation;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.firefox.FirefoxDriver;

import cucumber.api.PendingException;
import cucumber.api.java.en.Given;
import cucumber.api.java.en.Then;
import cucumber.api.java.en.When;

public class TestSteps {
	WebDriver driver;
	@Given("^on homepage$")
	public void on_homepage() throws Throwable {
	    driver = new FirefoxDriver();
	    driver.get("localhost:7001/LDS");
	}

	//Phase 2
	@When("^select a commit$")
	public void select_a_commit() throws Throwable {
		//change xpath
		driver.findElement(By.xpath("/html/body/div[2]/form/div/div/div/div/table/tbody/tr[2]/td[3]/input")).click();
	}

	@When("^click edit commit$")
	public void click_edit() throws Throwable {
		driver.findElement(By.xpath("/html/body/div[2]/form/div/div/div/div/input[2]")).submit();
	}

	@Then("^the client can select a loan$")
	public void the_client_can_select_a_loan() throws Throwable {
	    driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/div[1]/table/tbody/tr[2]/td[13]/input")).click();
	}
	@Then("^the client can select a loan to remove$")
	public void the_client_can_select_a_loan_to_remove() throws Throwable {
		driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/div[2]/table/tbody/tr[2]/td[13]/input")).click();
    }
	

	@Then("^click add loan to commit$")
	public void click_add_loan_to_commit() throws Throwable {
		driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/div[1]/input")).submit();
	}

	@Then("^click new commit$")
	public void click_new_commit() throws Throwable {
		driver.findElement(By.xpath("/html/body/div[2]/form/div/div/div/div/input[1]")).submit();
	}

	@When("^click on new lender$")
	public void click_on_new_lender() throws Throwable {
		driver.findElement(By.xpath("/html/body/div[2]/form/div/div/div/div/input")).submit();
	}

	@When("^click on new loan$")
	public void click_on_new_loan() throws Throwable {
		driver.findElement(By.xpath("/html/body/div[2]/div[2]/div[1]/form/input")).submit();
	}

	@When("^click on edit commit$")
	public void click_on_edit_commit() throws Throwable {
	    driver.findElement(By.xpath("/html/body/div[2]/form/div/div/div/div/input[2]")).submit();
	}

	@When("^select a loan$")
	public void select_a_loan() throws Throwable {
		driver.findElement(By.xpath("/html/body/div[2]/form/div/div/div/div/table/tbody/tr[2]/td[13]/input")).click();
	}

	@When("^click on edit loans$")
	public void click_on_edit_loans() throws Throwable {
	   driver.findElement(By.xpath("/html/body/div[2]/form/div/div/div/div/input[2]")).submit();
	}

	@Then("^click remove loan from commit$")
	public void click_remove_loan_from_commit() throws Throwable {	    
	    driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/div[2]/input")).submit();
	}

	@Then("^click submit commit$")
	public void click_submit() throws Throwable {
		driver.findElement(By.xpath("/html/body/div[2]/form/div/div/div/div/input[4]")).submit();
	}
	@Then("^close browser$")
	public void close_browser() throws Throwable {
		driver.quit();
	}
	@When("^click on commit management$")
	public void click_on_commit_management() throws Throwable {
		driver.findElement(By.xpath("/html/body/div[2]/div[2]/div[2]/form/input")).submit();
	}

	@When("^click on lender management$")
	public void click_on_lender_management() throws Throwable {
		driver.findElement(By.xpath("/html/body/div[2]/div[2]/div[3]/form/input")).submit();
	}

	@Then("^the client can create a lender$")
	public void the_client_can_create_a_lender() throws Throwable {
		driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/input[1]")).sendKeys("test lender");
		driver.findElement(By.xpath("/html/body/div[2]/div[2]/div[1]/form/input[5]")).submit();
	}

	@When("^click on loan management$")
	public void click_on_loan_management() throws Throwable {
		driver.findElement(By.xpath("/html/body/div[2]/div[2]/div[1]/form/input")).submit();
	}

	@Then("^the client can create a loan$")
	public void the_client_can_create_a_loan() throws Throwable {
	    driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/input[1]")).sendKeys("test");
	    driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/input[2]")).sendKeys("test");
	    driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/input[3]")).sendKeys("test");
	    driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/input[4]")).sendKeys("test");
	    driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/input[5]")).sendKeys("test");
	    driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/input[6]")).sendKeys("test");
	    driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/input[7]")).sendKeys("test");
	    driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/input[8]")).sendKeys("test");
	    driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/input[9]")).sendKeys("test");
	    driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/input[10]")).click();
		driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form")).submit();
	}

	@Then("^the client can edit the commit$")
	public void the_client_can_edit_the_commit() throws Throwable {
	    // Write code here that turns the phrase above into concrete actions
	    //throw new PendingException();
		//this feature file isnt really needed since i test add and remove seperately
	}

	@Then("^the client can edit the loans$")
	public void the_client_can_edit_the_loans() throws Throwable {
		 driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/input[1]")).sendKeys("edit");
		    driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/input[2]")).sendKeys("edit");
		    driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/input[3]")).sendKeys("edit");
		    driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/input[4]")).sendKeys("edit");
		    driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/input[5]")).sendKeys("edit");
		    driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/input[6]")).sendKeys("edit");
		    driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/input[7]")).sendKeys("edit");
		    driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/input[8]")).sendKeys("edit");
		    driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/input[9]")).sendKeys("edit");
		    driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form/input[10]")).click();
			driver.findElement(By.xpath("/html/body/div[2]/div[2]/div/form")).submit();
	}


}
