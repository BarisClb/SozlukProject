import createRootReducer from "./reducers";
import { createBrowserHistory } from "history";
import {
	persistStore,
	persistReducer,
	FLUSH,
	REHYDRATE,
	PAUSE,
	PERSIST,
	PURGE,
	REGISTER,
} from "redux-persist";
import storage from "redux-persist/lib/storage";
import { configureStore } from "@reduxjs/toolkit";

export const storeManager = {
	store: null,
	history: null,
	persistor: null,
	configureStore(basePath) {
		this.history = createBrowserHistory({ baseName: basePath });
		var persistedReducer = persistReducer(
			{ key: "root", storage },
			createRootReducer(this.history)
		);

		this.store = configureStore({
			reducer: persistedReducer,
			middleware: (getDefaultMiddleware) =>
				getDefaultMiddleware({
					serializableCheck: {
						ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER],
					},
				}),
		});

		this.persistor = persistStore(this.store);

		return this.store;
	},
};

export default storeManager;
