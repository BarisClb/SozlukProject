import React from "react";
import ReactDOM from "react-dom/client";
import reportWebVitals from "./reportWebVitals";
import { storeManager } from "./store/index";
import { Provider } from "react-redux";
import { PersistGate } from "redux-persist/integration/react";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/js/bootstrap.min.js";
import { BrowserRouter, Route, Routes } from "react-router-dom";

import CommonMain from "./screens/common/CommonMain";
import Welcome from "./screens/common/Welcome";

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
	<Provider store={storeManager.configureStore()}>
		<PersistGate loading={false} persistor={storeManager.persistor}>
			<BrowserRouter>
				<Routes>
					{/* I use a single 'roof' Route to access all the related routes (For common checks, like 'darkMode' and Auth purposes, etc) */}
					<Route path="/" element={<CommonMain />}>
						<Route path="/" element={<Welcome />} />
					</Route>
				</Routes>
			</BrowserRouter>
		</PersistGate>
	</Provider>
);

reportWebVitals();
