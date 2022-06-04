import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import "./css/index.css";
import { commonActions } from "../../store/actions/commonActions";

function PageOne() {
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
		<div id="page-one-wrapper">
			<h1>This is Page One!</h1>
			<button className="btn btn-success" onClick={() => loadingStart()}>
				Check Loading Screen!
			</button>
		</div>
	);
}

export default PageOne;
