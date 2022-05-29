import React from "react";
import "./css/index.css";
import MainNavigation from "../navigation/MainNavigation";
import { useSelector } from "react-redux";
import Loading from "../common/Loading";

function MainLayout(props) {
	// You can use the Loading screen either here or in the Route 'roof', your choice
	const isLoading = useSelector((state) => state.common.IsLoading);
	return (
		<>
			<MainNavigation />
			{isLoading && <Loading />}
			<div className="main-content-wrapper">{props.children}</div>
		</>
	);
}

export default MainLayout;
