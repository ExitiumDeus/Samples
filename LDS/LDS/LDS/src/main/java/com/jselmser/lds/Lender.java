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
@Table(name = "LENDER")
public class Lender implements Serializable{
	
	/**
	 * 
	 */
	private static final long serialVersionUID = -320191764248532258L;
	@Id
	@Column(name = "LENDER_ID")
	@SequenceGenerator(name="LEND_SEQ", sequenceName = "DB_LEND_SEQ")
	@GeneratedValue(generator="LEND_SEQ",strategy = GenerationType.SEQUENCE)
	private int lenderId;
	@Column(name = "LENDERNAME")
	private String lenderName;
	//1:m
	@OneToMany(cascade=CascadeType.MERGE, fetch = FetchType.EAGER, mappedBy="lenderId")
	private List<Loan> listOfLoans;
	
	
	
	public Lender() {
		super();
	}
	public Lender(String lenderName) {
		super();
		//this.lenderId = lenderId;
		this.lenderName = lenderName;
		//this.listOfLoans = listOfLoans;
	}
	public int getLenderId() {
		return lenderId;
	}
	public void setLenderId(int lenderId) {
		this.lenderId = lenderId;
	}
	public String getLenderName() {
		return lenderName;
	}
	public void setLenderName(String lenderName) {
		this.lenderName = lenderName;
	}
	public List<Loan> getListOfLoans() {
		return listOfLoans;
	}
	public void setListOfLoans(List<Loan> listOfLoans) {
		this.listOfLoans = listOfLoans;
	}
	@Override
	public String toString() {
		return "Lender [lenderId=" + lenderId + ", lenderName=" + lenderName + ", listOfLoans=" + listOfLoans + "]";
	}
	
}
