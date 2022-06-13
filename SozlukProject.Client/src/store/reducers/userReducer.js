import { userTypes } from "../types/userTypes";

export default function userReducer(state = { users: [], singleUser: {} }, action) {
	switch (action.type) {
		case userTypes.GetUsers:
			return { ...state, users: action.payload };

		case userTypes.GetSingleUser:
			return { ...state, singleUser: action.payload };

		case userTypes.CreateUser:
			return state;

		case userTypes.UpdateUser:
			return state;

		case userTypes.DeleteUser:
			return state;

		default:
			return state;
	}
}
