import React from "react";
import { Link } from "react-router-dom";
import "./css/index.css";

function MainNavigation() {
	return (
		<nav id="main-navigation" className="nav nav-pills flex-column flex-sm-row">
			<Link to="/pageone">Page One</Link>
			<Link to="/">
				<h2>Main Page</h2>
			</Link>
			<Link to="/pagetwo">Page Two</Link>
		</nav>
	);
}

export default MainNavigation;
