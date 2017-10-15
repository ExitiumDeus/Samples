package com.jselmser.lds;

import java.io.Serializable;
import java.math.BigDecimal;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.SequenceGenerator;
import javax.persistence.Table;

@Entity
@Table(name = "LOAN")
public class Loan implements Serializable{

	/**
	 * 
	 */
	private static final long serialVersionUID = 9012180821022979614L;

	@Id
	@Column(name = "LOAN_ID")
	@SequenceGenerator(name="LOAN_SEQ", sequenceName = "DB_LOAN_SEQ")
	@GeneratedValue(generator="LOAN_SEQ",strategy = GenerationType.SEQUENCE)
	private int loanId;
	
	@ManyToOne(cascade=CascadeType.MERGE, fetch = FetchType.EAGER)
	@JoinColumn(name = "LENDER_ID")
	private Lender lenderId;
	
	@ManyToOne(cascade=CascadeType.MERGE, fetch=FetchType.EAGER)
	@JoinColumn(name = "COMMITMENT_ID")
	private Commit commitId;
	
	@Column(name = "LOAN_TERM")
	private int loanTerm;
	
	@Column(name = "PROPERTY_ADDRESS")
	private String propertyAddress;
	
	@Column(name = "PROPERTY_CITY")
	private String propertyCity;
	
	@Column(name = "PROPERTY_STATE")
	private String propertyState;
	
	@Column(name = "PROPERTY_ZIP")
	private String propertyZip;
	
	@Column(name = "BORROWER_NAME")
	private String borrowerName;
	
	@Column(name = "ARM")
	private boolean ARM;
	
	@Column(name = "RATE")
	private double rate;
	
	@Column(name = "AMOUNT")
	private BigDecimal amount;
	
	@Column(name = "ARCHIVED")
	private boolean archived;
	public Loan() {
		super();
	}
	
	
	public Loan(Lender lenderId, Commit commitId, int loanTerm, String propertyAddress, String propertyCity,
			String propertyState, String propertyZip, String borrowerName, boolean aRM, double rate, BigDecimal amount,
			boolean archived) {
		super();
		//this.loanId = loanId;
		this.lenderId = lenderId;
		this.commitId = commitId;
		this.loanTerm = loanTerm;
		this.propertyAddress = propertyAddress;
		this.propertyCity = propertyCity;
		this.propertyState = propertyState;
		this.propertyZip = propertyZip;
		this.borrowerName = borrowerName;
		ARM = aRM;
		this.rate = rate;
		this.amount = amount;
		this.archived = archived;
	}


	public Commit getCommitId() {
		return commitId;
	}
	public void setCommitId(Commit commitId) {
		this.commitId = commitId;
	}

	
	public int getLoanId() {
		return loanId;
	}
	public void setLoanId(int loanId) {
		this.loanId = loanId;
	}
	public Lender getLenderId() {
		return lenderId;
	}
	public void setLenderId(Lender lender) {
		this.lenderId = lender;
	}
	public int getLoanTerm() {
		return loanTerm;
	}
	public void setLoanTerm(int loanTerm) {
		this.loanTerm = loanTerm;
	}
	public String getPropertyAddress() {
		return propertyAddress;
	}
	public void setPropertyAddress(String propertyAddress) {
		this.propertyAddress = propertyAddress;
	}
	public String getPropertyCity() {
		return propertyCity;
	}
	public void setPropertyCity(String propertyCity) {
		this.propertyCity = propertyCity;
	}
	public String getPropertyState() {
		return propertyState;
	}
	public void setPropertyState(String propertyState) {
		this.propertyState = propertyState;
	}
	public String getPropertyZip() {
		return propertyZip;
	}
	public void setPropertyZip(String propertyZip) {
		this.propertyZip = propertyZip;
	}
	public String getBorrowerName() {
		return borrowerName;
	}
	public void setBorrowerName(String borrowerName) {
		this.borrowerName = borrowerName;
	}
	public boolean isARM() {
		return ARM;
	}
	public void setARM(boolean aRM) {
		ARM = aRM;
	}
	public double getRate() {
		return rate;
	}
	public void setRate(double rate) {
		this.rate = rate;
	}
	public BigDecimal getAmount() {
		return amount;
	}
	public void setAmount(BigDecimal amount) {
		this.amount = amount;
	}
	public boolean isArchived() {
		return archived;
	}
	public void setArchived(boolean archived) {
		this.archived = archived;
	}


	@Override
	public String toString() {
		return "Loan [loanId=" + loanId  + ", loanTerm=" + loanTerm
				+ ", propertyAddress=" + propertyAddress + ", propertyCity=" + propertyCity + ", propertyState="
				+ propertyState + ", propertyZip=" + propertyZip + ", borrowerName=" + borrowerName + ", ARM=" + ARM
				+ ", rate=" + rate + ", amount=" + amount + ", archived=" + archived + "]";
	}

	
	
	
}
