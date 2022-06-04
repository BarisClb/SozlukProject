import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import "./css/index.css";
import { commonActions } from "../../store/actions/commonActions";

function PageTwo() {
	const dispatch = useDispatch();
	const loadingStart = () => {
		dispatch(commonActions.asyncStarted());
	};
	const loadingEnd = () => {
		dispatch(commonActions.asyncEnd());
	};
	const isLoading = useSelector((state) => state.common.IsLoading);
	useEffect(() => {
		if (isLoading) {
			setTimeout(() => loadingEnd(), 5000);
		}
	}, [isLoading]);
	useEffect(() => {
		if (!isLoading) {
			loadingEnd();
		}
	}, []);
	return (
		<div id="page-two-wrapper">
			<h1>This is Page Two!</h1>
			<button className="btn btn-success" onClick={() => loadingStart()}>
				Check Loading Screen!
			</button>
		</div>
	);
}

export default PageTwo;
