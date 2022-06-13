import { commonTypes } from "../types/commonTypes";

let loadingQueue = 0;

const asyncStart = () => {
	loadingQueue += 1;
	console.log(loadingQueue);
	return async (dispatch) => {
		dispatch({ type: commonTypes.AsyncStarted });
	};
};

const asyncEnd = () => {
	return async (dispatch) => {
		if (loadingQueue > 1) {
			loadingQueue -= 1;
		} else {
			dispatch({ type: commonTypes.AsyncEnd });
		}
	};
};

const baseRequest = (request) => {
	asyncStart();
	request();
	asyncEnd();
};

const getRequest = async (url, dispatchAction, dispatchType, successCallback, errorCallback) => {
	console.log("third");
	asyncStart();
	try {
		let response = await fetch(url);
		let responseJson = await response.json();
		if (responseJson.success) {
			// toast(responseJson.message);
			dispatchAction({ type: dispatchType, payload: responseJson.data });
			if (successCallback) {
				successCallback();
			}
		} else {
			// toast.warning(responseJson.message);
			if (errorCallback) {
				errorCallback();
			}
		}
	} catch (error) {
		// toast.warning(error);
		if (errorCallback) {
			errorCallback();
		}
	}

	asyncEnd();
};

const postRequest = (
	url,
	requestBody,
	dispatchAction,
	dispatchType,
	successCallback,
	errorCallback
) => {
	return async (dispatch) => {
		asyncStart();
		try {
			let response = await fetch(url, {
				method: "POST",
				headers: {
					"Content-Type": "application/json",
				},
				body: JSON.stringify({
					...requestBody,
				}),
			});
			let responseJson = await response.json();
			if (responseJson.success) {
				// toast(responseJson.message);
				dispatchAction({ type: dispatchType, payload: responseJson.data });
				if (successCallback) {
					successCallback();
				}
			} else {
				// toast.warning(responseJson.message);
				if (errorCallback) {
					errorCallback();
				}
			}
		} catch (error) {
			// toast.warning(error);
			if (errorCallback) {
				errorCallback();
			}
		}

		asyncEnd();
	};
};

const putRequest = (url, dispatchType, successCallback, errorCallback) => {
	return async (dispatch) => {
		dispatch(dispatch);
	};
};

const deleteRequest = (url, dispatchType, successCallback, errorCallback) => {
	return async (dispatch) => {
		dispatch(dispatch);
	};
};

export const apiHelpers = {
	getRequest,
	postRequest,
	putRequest,
	deleteRequest,
};
