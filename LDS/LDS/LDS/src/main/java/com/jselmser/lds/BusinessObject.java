package com.jselmser.lds;

import java.math.BigDecimal;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Isolation;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;

@Service("BusinessObject")
public class BusinessObject { //bo called in handler servlet?
	//Dao
	@Autowired
	OracleDAO oracleDao;

	
	//views -- business logic to change the collection of these elemtns and display them in the jsp
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public List<Loan> ViewLoan(){
		//get list from dao? then send to jsp and format on that page?
		return oracleDao.FindAllLoans();
	}
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public List<Loan> ViewArchiveLoan(){
		return oracleDao.FindArchivedLoans();
	}
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public List<Commit> ViewArchiveCommit(){
		return oracleDao.FindArchivedCommits();
	}
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public List<Commit> ViewCommit(){
		return oracleDao.FindAllCommits();
	}
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class,isolation=Isolation.READ_COMMITTED)
	public List<Lender> ViewLender(){
		return oracleDao.FindAllLenders();
	}
	
//	//creates -- business logic to create one of these objects and then store them with the DAOSAVE
//	int loanId, Lender lender, int loanTerm, String propertyAddress, String propertyCity,
//	String propertyState, String propertyZip, String borrowerName, boolean aRM, double rate, BigDecimal amount,
//	boolean archived)
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public void CreateLoan(Lender lender, int loanTerm, String propertyAddress, String propertyCity,
			String propertyState, String propertyZip, String borrowerName, boolean ARM, double rate, BigDecimal amount,
			boolean archived){
		oracleDao.SaveLoan(new Loan(lender,null,loanTerm,propertyAddress,propertyCity,propertyState,
				propertyZip,borrowerName,ARM,rate,amount,archived));
	}
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public void CreateCommit(){
		oracleDao.SaveCommit(new Commit());
	}
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public void CreateCommit(String name){
		oracleDao.SaveCommit(new Commit(name));
	}
	
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)	
	public void CreateLender(String name){
		oracleDao.SaveLender(new Lender(name));
	}
	//edits -- business logic to edit one of these objects and then store them with the DAOSAVE
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public void EditLoan(int loanId, Lender lender, int loanTerm, String propertyAddress, String propertyCity,
			String propertyState, String propertyZip, String borrowerName, boolean ARM, double rate, BigDecimal amount,
			boolean archived){
		//findbyidreplacestuffthensave
		
		Loan l = oracleDao.FindLoanById(loanId, Loan.class);
		l.setLenderId(lender);
		l.setLoanTerm(loanTerm);
		l.setPropertyAddress(propertyAddress);
		l.setPropertyCity(propertyCity);
		l.setPropertyState(propertyState);
		l.setPropertyZip(propertyZip);
		l.setBorrowerName(borrowerName);
		l.setARM(ARM);
		l.setRate(rate);
		l.setAmount(amount);
		l.setArchived(archived);
		
		oracleDao.SaveLoan(l);
	}
	
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public void EditLender(int id, String name){
		Lender l = oracleDao.FindLenderById(id, Lender.class);
		l.setLenderName(name);
		oracleDao.SaveLender(l);
	}
	//other -- business logic
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public void AddLoanToCommmit(int loanId, int commitId){
		Commit c = oracleDao.FindCommitById(commitId, Commit.class);
		Loan l = oracleDao.FindLoanById(loanId, Loan.class);
		if(!c.getListOfLoans().contains(l)){
			c.getListOfLoans().add(l);
			l.setCommitId(c);
		}
	}
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public void RemoveLoanFromCommit(int loanId, int commitId){
		Commit c = oracleDao.FindCommitById(commitId, Commit.class);
		Loan l = oracleDao.FindLoanById(loanId, Loan.class);
		if(c.getListOfLoans().contains(l)){
			c.getListOfLoans().remove(l);
			l.setCommitId(null);
		}
	}
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public void SubmitCommit(int commitId){
		//find commit, check it vs business rules, then archive?
		//foreach loan in commit
		//apply business rules
		//if fail don't archive?
		boolean test = true; //if false at the end then the submission fails
		float sum = 0.0f;
		System.out.println(oracleDao.FindCommitById(commitId, Commit.class).getListOfLoans().size());
			if(oracleDao.FindCommitById(commitId, Commit.class).getListOfLoans().size() > 0){
				Lender testLender = oracleDao.FindCommitById(commitId, Commit.class).getListOfLoans().get(0).getLenderId();
				System.out.println(testLender.getLenderName());
				for(Loan l : oracleDao.FindCommitById(commitId, Commit.class).getListOfLoans()){
				sum += l.getAmount().floatValue();
				System.out.println("The sum is " + sum);
				
				if(!(testLender.getLenderName().equals(l.getLenderId().getLenderName()))){ //All from the same lender
					test = false;
				}
				if(!(l.getRate() >= 5)){ //if rate isnt above a certain amount
					test = false;
				}
				
			}
			if(!(sum >= 1000000)){
				test = false;
			}
			if(test){
				//test pass, archive commit and loans
				for(Loan l : oracleDao.FindCommitById(commitId, Commit.class).getListOfLoans()){
					l.setArchived(true);
				}
				oracleDao.FindCommitById(commitId, Commit.class).setArchived(true);
			}else{
				oracleDao.FindCommitById(commitId, Commit.class).setArchived(false);
			}
		}
	}
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public void DeleteLoan(int i){
		oracleDao.FindLoanById(i, Loan.class).setArchived(true);
		oracleDao.SaveLoan(oracleDao.FindLoanById(i, Loan.class));
	}
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public void DeleteCommit(int i){
		oracleDao.FindCommitById(i, Commit.class).setArchived(true);
		oracleDao.SaveCommit(oracleDao.FindCommitById(i, Commit.class));
	}
//  --DaoImpl--
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public Loan FindTestLoan(){
		return oracleDao.FindTestLoan();
	}
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public Lender FindTestLender(){
		return oracleDao.FindTestLender();
	}
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public Commit FindTestCommit(){
		return oracleDao.FindTestCommit();
	}
	//
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public Loan FindLoanById(int i){
		return oracleDao.FindLoanById(i, Loan.class);
	}
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public Commit FindCommitById(int i){
		return oracleDao.FindCommitById(i, Commit.class);
	}
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public Lender FindLenderById(int i){
		return oracleDao.FindLenderById(i, Lender.class);
	}
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public void SaveLoan(Loan loan){
		oracleDao.SaveLoan(loan);
	}
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public void SaveLender(Lender lender){
		oracleDao.SaveLender(lender);
	}
	@Transactional(propagation=Propagation.REQUIRES_NEW,rollbackFor=Exception.class)
	public void SaveCommit(Commit commit){
		oracleDao.SaveCommit(commit);
	}
	@Override
	public String toString() {
		return "BusinessObject [oracleDao=" + oracleDao + "]";
	}
	
	

}
