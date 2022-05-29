import React from "react";
import { useSelector } from "react-redux";
import { Outlet } from "react-router-dom";
import Loading from "../../components/common/Loading";
import MainLayout from "../../components/layout/MainLayout";

function CommonMain() {
	// You can use the Loading screen either here or in the Layout, your choice
	const isLoading = useSelector((state) => state.common.IsLoading);
	return (
		<MainLayout>
			{isLoading && <Loading />} <Outlet />
		</MainLayout>
	);
}

export default CommonMain;
