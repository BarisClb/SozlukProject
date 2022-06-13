import React, { useRef } from "react";
import { Link, useNavigate } from "react-router-dom";
import "./css/index.css";

function MainNavigation() {
	const navigate = useNavigate();
	let searchValue = useRef("");
	const searchClick = () => {
		navigate(`/${searchValue.current}`);
	};
	return (
		<div id="main-navigation" className="nav flex-row">
			<div id="main-navigation-row" className="row">
				<div id="navbar-logo-section" className="col-sm-4">
					<img alt="logo" src="/CelebiSozlukLogo.png" id="main-navigation-logo" />
				</div>
				<div id="navbar-searchbar-section" className="col-sm-8 col-md-6">
					<form className="d-flex">
						<input
							className="form-control me-2"
							type="search"
							placeholder="Search Product"
							aria-label="Search"
							defaultValue={searchValue.current}
							onChange={(e) => (searchValue.current = e.target.value)}
						/>
						<div className="btn btn-outline-primary" onClick={() => searchClick()}>
							Search
						</div>
					</form>
				</div>
				<div id="navbar-profile-section" className="col">
					<div className="col">Profile</div>
					<div className="col">Sign Out</div>
				</div>
			</div>
		</div>
	);
}

export default MainNavigation;
