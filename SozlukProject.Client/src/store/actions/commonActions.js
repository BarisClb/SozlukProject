import { commonTypes } from "../types/commonTypes";

const asyncStarted = () => {
	return {
		type: commonTypes.AsyncStarted,
	};
};

const asyncEnd = () => {
	return {
		type: commonTypes.AsyncEnd,
	};
};

const clearError = () => {
	return {
		type: commonTypes.ClearError,
	};
};

const toggleDarkMode = (darkMode) => {
	return {
		type: commonTypes.DarkMode,
		payload: darkMode,
	};
};

export const commonActions = {
	clearError,
	asyncStarted,
	asyncEnd,
	toggleDarkMode,
};
