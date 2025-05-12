import 'package:flavortalk_app/features/sign_in/sign_in_page.dart';
import 'package:flavortalk_app/pages/home_page.dart';
import 'package:flavortalk_app/pages/root_page.dart';
import 'package:go_router/go_router.dart';

final router = GoRouter(
  routes: <RouteBase>[
    GoRoute(
      path: Routes.root,
      builder: (context, state) => const RootPage(),
    ),
    GoRoute(
      path: Routes.signIn,
      builder: (context, state) => const SignInPage(),
    ),
    GoRoute(
      path: Routes.home,
      builder: (context, state) => const HomePage(),
    ),
  ],
);

abstract class Routes {
  static const root = '/';
  static const signIn = '/signin';
  static const home = '/home';
}
