package com.jselmser.lds;

import java.io.Serializable;
import java.util.List;

import javax.inject.Inject;

import org.hibernate.SessionFactory;
import org.hibernate.Transaction;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Propagation;
import org.springframework.transaction.annotation.Transactional;

@Repository("OracleDAO")
public class OracleDAO implements GenericDAO{
//	
//	@Inject
//	SessionFactory sessionFactory;
	
	private final SessionFactory sessionFactory;
	
	public SessionFactory getSessionFactory() {
		return this.sessionFactory;
	}
	
	//autowire? or do bean implementation
	@Autowired
	public OracleDAO(SessionFactory sessionFactory){
		this.sessionFactory = sessionFactory;
	}

	
	public Loan FindLoanById(int i, Class type) {
		Loan loan = (Loan)getSessionFactory().getCurrentSession().get(type, (Serializable)Integer.valueOf(i));
		return loan;
	}
	
	public Commit FindCommitById(int i, Class type) {
		Commit commit = (Commit)getSessionFactory().getCurrentSession().get(type, (Serializable)Integer.valueOf(i));
		return commit;
	}

	
	public Lender FindLenderById(int i, Class type) {
		Lender lender = (Lender)getSessionFactory().getCurrentSession().get(type, (Serializable)Integer.valueOf(i));
	
		return lender;
	}

	public void SaveLoan(Loan loan) {
		//save or update depending on if it exists yet 
		 getSessionFactory().getCurrentSession().saveOrUpdate(loan);
	}
	
	public void SaveLender(Lender lender) {
		 getSessionFactory().getCurrentSession().saveOrUpdate(lender);
	}
	
	public void SaveCommit(Commit commit) {
		//save or update depending on if it exists yet 
		getSessionFactory().getCurrentSession().saveOrUpdate(commit);
	}

	@Override
	public String toString() {
		return "OracleDAO []";
	}
	public Lender FindTestLender(){
		return (Lender) getSessionFactory().getCurrentSession().createQuery("from Lender where lenderName = :name").setParameter("name", "testLender").list().get(0);
	}
	public Loan FindTestLoan(){
		return (Loan) getSessionFactory().getCurrentSession().createQuery("from Loan where borrowerName = :name").setParameter("name", "testBorrower").list().get(0);
	}
	public Commit FindTestCommit(){
		return (Commit) getSessionFactory().getCurrentSession().createQuery("from Commit where commitName = :name").setParameter("name", "testCommit").list().get(0);
	}
	
	public List<Loan> FindAllLoans() {
		// TODO Auto-generated method stub
		return getSessionFactory().getCurrentSession().createQuery("from Loan where archived = false and commitId = null").list();
		
	}

	
	public List<Loan> FindArchivedLoans() {
		// TODO Auto-generated method stub
		return getSessionFactory().getCurrentSession().createQuery("from Loan where archived = true").list();
	}

	
	public List<Commit> FindAllCommits() {
		// TODO Auto-generated method stub
		return getSessionFactory().getCurrentSession().createQuery("from Commit where archived = false").list();
	}

	
	public List<Commit> FindArchivedCommits() {
		// TODO Auto-generated method stub
		return getSessionFactory().getCurrentSession().createQuery("from Commit where archived = true").list();
	}

	
	public List<Lender> FindAllLenders() {
		// TODO Auto-generated method stub
		return getSessionFactory().getCurrentSession().createQuery("from Lender").list();
	}
}
