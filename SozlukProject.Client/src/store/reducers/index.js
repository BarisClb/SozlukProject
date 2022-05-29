import { combineReducers } from "redux";
import commonReducer from "./commonReducer";

const createRootReducer = () =>
	combineReducers({
		common: commonReducer,
	});

export default createRootReducer;
