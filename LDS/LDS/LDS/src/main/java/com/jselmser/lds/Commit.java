package com.jselmser.lds;

import java.io.Serializable;
import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.OneToMany;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;

@Entity
@Table(name="COMMIT")
public class Commit implements Serializable{
	
	/**
	 * 
	 */
	private static final long serialVersionUID = -1519367039433995724L;

	@Id
	@Column(name = "COMMIT_ID")
	@SequenceGenerator(name="COMMIT_SEQ", sequenceName = "DB_COMMIT_SEQ")
	@GeneratedValue(generator="COMMIT_SEQ",strategy = GenerationType.SEQUENCE)
	private int commitId;
	
	@OneToMany(cascade=CascadeType.MERGE, fetch = FetchType.EAGER, mappedBy="commitId")
	private List<Loan> listOfLoans;
	
	@Column(name = "ARCHIVED")
	private boolean archived;
	
	@Column(name = "COMMIT_NAME")
	private String commitName;
	
	public Commit() {
		super();
	}

	public Commit(List<Loan> listOfLoans) {
		super();
		this.listOfLoans = listOfLoans;
	}
	public Commit(String commitName) {
		super();
		//this.listOfLoans = listOfLoans;
		this.commitName = commitName;
	}

	public int getCommitId() {
		return commitId;
	}
	
	public void setCommitId(int commitId) {
		this.commitId = commitId;
	}

	public List<Loan> getListOfLoans() {
		return listOfLoans;
	}

	public void setListOfLoans(List<Loan> listOfLoans) {
		this.listOfLoans = listOfLoans;
	}
	public boolean isArchived() {
		return archived;
	}
	public void setArchived(boolean archived) {
		this.archived = archived;
	}
	

	public String getCommitName() {
		return commitName;
	}

	public void setCommitName(String commitName) {
		this.commitName = commitName;
	}

	@Override
	public String toString() {
		return "Commit [commitId=" + commitId + ", listOfLoans=" + listOfLoans + ", archived=" + archived + "]";
	}


}
