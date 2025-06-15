import 'package:flavortalk_app/pages/home_page.dart';
import 'package:flavortalk_app/pages/root_page.dart';
import 'package:flavortalk_app/pages/sign_in_page.dart';
import 'package:flavortalk_app/pages/sign_up_page.dart';
import 'package:go_router/go_router.dart';

final router = GoRouter(
  routes: <RouteBase>[
    GoRoute(path: Routes.root, builder: (context, state) => const RootPage()),
    GoRoute(
      path: Routes.signIn,
      builder: (context, state) => const SignInPage(),
    ),
    GoRoute(path: Routes.home, builder: (context, state) => const HomePage()),
    GoRoute(
      path: Routes.signUp,
      builder: (context, state) => const SignUpPage(),
    ),
  ],
);

abstract class Routes {
  static const root = '/';
  static const signIn = '/signin';
  static const signUp = '/signup';
  static const home = '/home';
}
