package com.jselmser.lds;

import java.util.List;

public interface GenericDAO {
	//change to generic class
	
	Loan FindLoanById(int i, Class type);
	Commit FindCommitById(int i, Class type);
	Lender FindLenderById(int i, Class type);
	List<Loan> FindAllLoans();
	List<Loan> FindArchivedLoans();
	List<Commit> FindAllCommits();
	List<Commit> FindArchivedCommits();
	List<Lender> FindAllLenders();
	void SaveLoan(Loan loan);
	void SaveLender(Lender lender);
	void SaveCommit(Commit commit);

}
